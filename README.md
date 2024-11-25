# Project: Product Popularity Determination System

## Project Description
This project is designed to determine product popularity using an expert system based on large language models (LLMs). It is implemented on **Microsoft Windows 11 Pro** using the following tools:

- [**MS Visual Studio Community 2022**](https://visualstudio.microsoft.com/)
- [**GPT4ALL**](https://www.nomic.ai/gpt4all)
- [**MS SQL Server Management Studio**](https://aka.ms/ssmsfullsetup)

## Development Environment Setup

### 1. Installing MS Visual Studio Community 2022
During the installation of **Visual Studio 2022**, select the following components:

#### **ASP.NET and web development**:
1. .NET desktop development
2. .NET Framework 4.8 development tools
3. Cloud tools for web development
4. .NET profiling tools
5. Entity Framework 6 tools
6. Live Share
7. .NET Debugging with WSL
8. IntelliCode

#### **.NET desktop development**:
1. Development tools for .NET
2. .NET Framework 4.8 development tools
3. Entity Framework 6 tools
4. .NET profiling tools
5. IntelliCode
6. Just-In-Time debugger
7. Live Share
8. ML.NET Model Builder
9. Blend for Visual Studio
10. SQL Server Express 2019 LocalDB
11. JavaScript diagnostics

---

### 2. Restoring the Database
To restore the database, use the `Diploma.bak` file.

1. Open **Microsoft SQL Server Management Studio**.
2. Create an account and connect to the server.

![Picture1](https://github.com/user-attachments/assets/5b5867fd-66de-4aaf-a7ed-cd3806a1c9b8)

3. Use the **View** menu to open the **Object Explorer**.

![Picture2](https://github.com/user-attachments/assets/db83d50a-1bf8-4021-ac8e-9c4c22914ef6)

4. In **Object Explorer**, right-click on the **Databases** node and select **Restore Database**.

![Picture3](https://github.com/user-attachments/assets/c5681ddc-95fc-4db1-83ed-5af5ded78547)

5. In the dialog, choose **Device** as the source and click the three-dot icon.

![Picture4](https://github.com/user-attachments/assets/0d5eb631-1d68-4833-bcac-97767860cbc1)

6. Add the `Diploma.bak` file by specifying its path and clicking **OK**.

![Picture5](https://github.com/user-attachments/assets/94ec61f6-adbf-4a31-81d5-2dba17778fd4)

7. Once the process is complete, the database will be restored.

---

### 3. Configuring the Database Connection
1. Open the `Diploma.sln` file in **Visual Studio 2022**.
2. Use the **View -> Solution Explorer** menu to locate and open the `appsettings.json` file.
3. Set the connection string to your database by replacing the `Server=` value with your server's name.

![Picture6](https://github.com/user-attachments/assets/f026fe72-fadf-444a-b289-298537596ccd)

---

### 4. Setting Up GPT4ALL
1. Install the GPT4ALL ecosystem.
2. On the **Models** tab, click **Add Model** and add the following models:
   - Nous Hermes 2 Mistral DPO
   - Mistral Instruct
   - Mistral OpenOrca

![Picture7](https://github.com/user-attachments/assets/2a4ad328-02c7-45f7-94be-28e4a4b45c03)
![Picture8](https://github.com/user-attachments/assets/389fc7e7-dca1-4b73-8c74-65c4a91e71b0)

3. Enable the GPT4ALL local API.

![Picture9](https://github.com/user-attachments/assets/5161eb55-e435-4a83-8af6-4a9336c98fd5)

---

### 5. Configuring the Client-Side Application
The client-side application uses **Node.js 16.13.2**. Ensure you have this version installed for optimal compatibility.

1. In **Solution Explorer** of **Visual Studio 2022** use the **View -> Solution Explorer** menu and right-click on the `diploma.client` project and select **Open in Terminal**.
2. In the terminal, run the following commands:
   ```bash
   npm install
   npm install antd



# Project Structure

## **Diploma.Server**
This is the backend application responsible for processing requests, managing data, and integrating with other components.

- **Controllers**
  - `EvaluationController.cs`: Handles requests related to product evaluations.
  - `ProductsController.cs`: Handles requests for product data retrieval.

- **Interfaces**
  - `IExpertEvaluationService.cs`: Interface for the service managing expert evaluations.
  - `IExpertRepository.cs`: Interface for accessing expert data.
  - `IExpertService.cs`: Interface for managing language models as experts.
  - `IFeatureEngineeringService.cs`: Interface for creating product features.
  - `IOpinionAgreementService.cs`: Interface for expert opinion agreement.
  - `IProductRepository.cs`: Interface for accessing product data.
  - `IProductService.cs`: Interface for managing product-related operations.

- **Migrations**
  - Contains database migration files for schema changes.

- **Models**
  - `ConsensusEvaluation.cs`: Represents agreed-upon evaluations from experts.
  - `DataContext.cs`: Database context for Entity Framework.
  - `ExpertEvaluation.cs`: Represents evaluations provided by expert models.
  - `LLMExpert.cs`: Represents a language model expert.
  - `Product.cs`: Represents a product in the system.

- **Properties**
  - `launchSettings.json`: Configuration for running the application in development.

- **Repositories**
  - `ExpertRepository.cs`: Implementation for accessing expert data.
  - `ProductRepository.cs`: Implementation for accessing product data.

- **Services**
  - `ExpertEvaluationService.cs`: Service for managing expert evaluations.
  - `ExpertService.cs`: Service for managing language model experts.
  - `FeatureEngineeringService.cs`: Service for calculating additional product features.
  - `OpinionAgreementService.cs`: Service for aligning expert opinions.
  - `ProductService.cs`: Service for managing product data.

- **Root Files**
  - `Diploma.Server.csproj`: Project file for the backend application.
  - `Diploma.Server.http`: Test HTTP requests for API endpoints.
  - `Program.cs`: Entry point for the backend application.
  - `RegressionModel.*`: Files related to using and training the regression model.
  - `appsettings.Development.json`: Development-specific application settings.
  - `appsettings.json`: General application settings.

---

## **DimplomaTest**
This project is for unit testing server-side services.

- `DimplomaTest.csproj`: Project file for the testing application.
- `ExpertEvaluationsServiceTests.cs`: Tests for the expert evaluation service.
- `ExpertServiceTests.cs`: Tests for the expert management service.
- `FeatureEngineeringServiceTests.cs`: Tests for the feature engineering service.
- `OpinionAgreementServiceTests.cs`: Tests for the opinion agreement service.
- `ProductServiceTests.cs`: Tests for the product management service.

---

## **diploma.client**
This is the frontend application for user interaction.

- **public**: Contains public assets for the frontend application.
- **src**: Contains the main source code for the frontend.
  - **api**
    - `api.ts`: General API configuration and setup.
    - `errorHandler.ts`: Handles API errors globally.
    - `evaluationApi.ts`: API methods for evaluations.
    - `productApi.ts`: API methods for products.
  - **components**
    - `Evaluation.tsx`: Component for rendering evaluation data.
    - `EvaluationResultsForm.tsx`: Form for displaying evaluation results.
    - `ExpertOpinionList.tsx`: Displays a list of expert opinions.
    - `LoadMoreButton.tsx`: Button to load more items.
    - `Opinion.tsx`: Component for rendering individual opinions.
    - `Product.tsx`: Component for displaying product data.
    - `ProductForm.tsx`: Form for submitting product data.
    - `ProductList.tsx`: Displays a list of products.
  - **models**: Contains TypeScript models used throughout the frontend.
  - `App.css`: Global styles for the application.
  - `App.tsx`: Main application component.
  - `config.ts`: Configuration settings for the frontend.
  - `index.css`: Global styles applied at the root level.
  - `main.tsx`: Entry point for the React application.
  - `vite-env.d.ts`: TypeScript declarations for Vite.

- **Root Files**
  - `.gitignore`: Files and folders to exclude from version control.
  - `diploma.client.esproj`: Project file for the frontend.
  - `eslint.config.js`: Configuration for ESLint.
  - `index.html`: Entry HTML file for the application.
  - `nuget.config`: Configuration for NuGet packages.
  - `package-lock.json`, `package.json`: Node.js dependencies and configuration.
  - `tsconfig.*`: TypeScript configuration files.
  - `vite.config.ts`: Configuration file for Vite.

---

## **Root Files**
- `.gitattributes`: Defines attributes for version-controlled files.
- `.gitignore`: Specifies files to be excluded from version control.
- `Diploma.bak`: Backup of the database.
- `Diploma.sln`: Solution file for Visual Studio.
- `README.md`: Documentation for the project.
