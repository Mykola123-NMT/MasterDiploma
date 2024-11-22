using Diploma.Server.Interfaces;
using Diploma.Server.Models;
using System.Text.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace Diploma.Server.Services
{
    public class ExpertEvaluationService : IExpertEvaluationService
    {
        public class ProductAdvantagesDisadvantages
        {
            public required string[] Advantages { get; set; }
            public required string[] Disadvantages { get; set; }
        }

        public class LLMResponse
        {
            public double priceStrategy { get; set; }
            public double demand { get; set; }
            public double quality { get; set; }
            public double priceQuality { get; set; }
        }
        
        private readonly HttpClient _httpClient;
        private readonly IExpertService _expertService;

        public ExpertEvaluationService(HttpClient httpClient, IExpertService expertService)
        {
            _httpClient = httpClient;
            _expertService = expertService;
        }

        public async Task<List<ExpertEvaluation>> GetOpinionsAsync(Product product)
        {
            var llmExperts = await _expertService.GetExpertsAsync();
            var productEvaluationResponses = new List<ExpertEvaluation>();
            _httpClient.Timeout = TimeSpan.FromSeconds(5000);

            foreach (var expert in llmExperts)
            {
                var productEvaluationPrompt = CreatePromptForProductEvaluation(product);

                var requestBody = new
                {
                    model = expert.Name,
                    messages = new[]
                    {
                    new { role = "user", content = productEvaluationPrompt }
                },
                    max_tokens = 350,
                    temperature = 0.7
                };

                var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(expert.URL, content);
                var responseJson = await response.Content.ReadAsStringAsync();

                var llmResponse = ParseEvaluationResponse(responseJson);
                if (llmResponse != null)
                {
                    llmResponse = ClampOpinionValues(llmResponse);
                    var evaluationResponse = new ExpertEvaluation
                    {
                        ProductId = product.Asin,
                        Product = product,
                        ExpertId = expert.ExpertId,
                        Expert = expert,
                        PriceStrategy = llmResponse.priceStrategy,
                        Demand = llmResponse.demand,
                        Quality = llmResponse.quality,
                        PriceQuality = llmResponse.priceQuality,
                        Timestamp = DateTime.Now
                    };
                    productEvaluationResponses.Add(evaluationResponse);
                }

                /*var advantagesDisadvantagesPrompt = CreatePromptForProductAdvantagesDisadvantages(product);

                var advantagesDisadvantagesRequestBody = new
                {
                    model = expert.Name,
                    messages = new[] { new { role = "user", content = advantagesDisadvantagesPrompt } },
                    max_tokens = 250,
                    temperature = 0.7
                };

                var advantagesDisadvantagesContent = new StringContent(JsonSerializer.Serialize(advantagesDisadvantagesRequestBody), Encoding.UTF8, "application/json");
                var advantagesDisadvantagesResponse = await _httpClient.PostAsync(expert.URL, advantagesDisadvantagesContent);
                var advantagesDisadvantagesResponseJson = await advantagesDisadvantagesResponse.Content.ReadAsStringAsync();

                var advantagesDisadvantages = ParseAdvantagesDisadvantages(advantagesDisadvantagesResponseJson);
                if (advantagesDisadvantages != null)
                {
                    // Додаємо переваги та недоліки до оцінки
                    if (productEvaluationResponses.Any())
                    {
                        var lastOpinion = productEvaluationResponses.Last();
                        lastOpinion.Advantages = advantagesDisadvantages.Advantages;
                        lastOpinion.Disadvantages = advantagesDisadvantages.Disadvantages;
                    }
                }*/
            }
            return productEvaluationResponses;
        }

        private string CreatePromptForProductEvaluation(Product product)
        {
            return $@"You are an AI assistant tasked with evaluating products. Based on the provided product information, perform the following evaluations and return the results strictly in JSON format. Your output must adhere to the structure and value constraints provided below, and any deviation will be invalid.

            ### Evaluation Metrics:
            1. **priceStrategy**: Assess the pricing strategy using the fields `price`, `listPrice`, and `Discount`. Provide a score between **0.0** and **10.0**.
            2. **demand**: Analyze the product's demand using the fields `stars` and `reviews`. Provide a score between **0.0** and **10.0**.
            3. **quality**: Evaluate the perceived product quality using `stars`, `reviews`, `isBestSeller`, and `boughtLastMonth`. Provide a score between **0.0** and **10.0**.
            4. **priceQuality**: Evaluate the price-to-quality ratio using `price`, `listPrice`, `Discount`, and `stars`. Provide a score between **0.0** and **10.0**.

            ### Product Details:
            - **Title**: {product.Title}
            - **Stars**: {product.Stars}
            - **Reviews**: {product.Reviews}
            - **Price**: {product.Price}
            - **ListPrice**: {product.ListPrice}
            - **Discount**: {product.Discount}
            - **IsBestSeller**: {product.IsBestSeller}

            ### Output Format:
            Your response must strictly adhere to this JSON structure:
            ```json
            {{
              ""priceStrategy"": score (0.0-10.0),
              ""demand"": score (0.0-10.0),
              ""quality"": score (0.0-10.0),
              ""priceQuality"": score (0.0-10.0)
            }}";
        }

        private string CreatePromptForProductAdvantagesDisadvantages(Product product)
        {
            return $@"You are an AI assistant tasked with evaluating products. Based on the provided product information, list three advantages and disadvantages of the product, formatted in JSON

            ### Product Details:
            - **Title**: {product.Title}
            - **Stars**: {product.Stars}
            - **Reviews**: {product.Reviews}
            - **Price**: {product.Price}
            - **ListPrice**: {product.ListPrice}
            - **LogPrice**: {product.LogPrice}
            - **Discount**: {product.Discount}
            - **IsBestSeller**: {product.IsBestSeller}

            ### Output Format:
            Your response must strictly adhere to this JSON structure:
            ```json
             {{
               ""Advantages"": [
                 ""Advantage 1"",
                 ""Advantage 2"",
                 ""Advantage 3""
               ],
               ""Disadvantages"": [
                 ""Disadvantage 1"",
                 ""Disadvantage 2"",
                 ""Disadvantage 3""
               ]
             }}";
        }

        private LLMResponse ClampOpinionValues(LLMResponse llmResponse)
        {
            llmResponse.priceStrategy = Math.Clamp(llmResponse.priceStrategy, 0.0, 10.0);
            llmResponse.demand = Math.Clamp(llmResponse.demand, 0.0, 10.0);
            llmResponse.quality = Math.Clamp(llmResponse.quality, 0.0, 10.0);
            llmResponse.priceQuality = Math.Clamp(llmResponse.priceQuality, 0.0, 10.0);

            return llmResponse;
        }

        public LLMResponse? ParseEvaluationResponse(string responseJson)
        {
            try
            {
                // Десеріалізація основного JSON-об'єкта
                var responseObject = JsonSerializer.Deserialize<JsonDocument>(responseJson);

                // Дістаємо вкладений JSON-текст у полі "content"
                string? content = responseObject
                    ?.RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();

                if (content != null)
                {
                    // Використання регулярного виразу для виділення JSON-об'єкта
                    var match = Regex.Match(content, @"\{.*\}", RegexOptions.Singleline);
                    if (match.Success)
                    {
                        string innerJson = match.Value;

                        // Десеріалізація вкладеного JSON-об'єкта в ExpertOpinion
                        var llmResponse = JsonSerializer.Deserialize<LLMResponse>(innerJson);
                        return llmResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing JSON: {ex.Message}");
            }

            return null;
        }

        public ProductAdvantagesDisadvantages? ParseAdvantagesDisadvantages(string responseJson)
        {
            try
            {
                // Десеріалізація основного JSON-об'єкта
                var responseObject = JsonSerializer.Deserialize<JsonDocument>(responseJson);

                // Дістаємо вкладений JSON-текст у полі "content"
                string? content = responseObject
                    ?.RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();

                if (content != null)
                {
                    // Використання регулярного виразу для виділення JSON-об'єкта
                    var match = Regex.Match(content, @"\{.*\}", RegexOptions.Singleline);
                    if (match.Success)
                    {
                        string innerJson = match.Value;

                        // Десеріалізація вкладеного JSON-об'єкта в ProductAdvantagesDisadvantages
                        var advantagesDisadvantages = JsonSerializer.Deserialize<ProductAdvantagesDisadvantages>(innerJson);
                        return advantagesDisadvantages;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing JSON: {ex.Message}");
            }

            return null;
        }
    }
}
