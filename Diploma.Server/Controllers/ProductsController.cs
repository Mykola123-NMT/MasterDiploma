using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Diploma.Server.Models;
using Diploma.Server.Interfaces;

namespace Diploma.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Products/GetProductsPerPage/5/5
        [HttpGet("GetProductsPerPage/{pageNumber}/{pageSize}")] 
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsPerPage(int pageNumber, int pageSize)
        {
            return await _productService.GetProductsByPageAsync(pageNumber, pageSize);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(string id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        private async Task<bool> ProductExists(string id)
        {
            return await _productService.ProductExistsAsync(id);
        }
    }
}
