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
