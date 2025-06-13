# ShareSafely - File Share Web App

A secure Azure-based web application that allows users to upload files to Azure Blob Storage and generates a time-limited share link. Includes a cleanup Logic App to automatically delete expired uploads.

---

##  Secure File Upload Workflow (Web App)
![WEBAPPP drawio](https://github.com/user-attachments/assets/15908e4f-516d-47bb-a91e-a13de6c2bbf9)

## Blob Cleanup Automation (Logic App)
![SecondDiagram](https://github.com/user-attachments/assets/af32aed7-74d3-47ed-8957-54a02159431b)



## Table of Contents

- [How it Works](#how-it-works)
- [Repository Layout](#repository-layout)
- [Setup & Deployment](#setup--deployment)
- [Usage](#usage)
- [Prerequisites](#prerequisites)
- [Contributing](#contributing)

---

## How it Works

1. User uploads a file via the web application.  
2. The app retrieves credentials securely from Azure Key Vault.  
3. Blob is stored in Azure Blob Storage.  
4. A secure SAS link is generated, valid for 30 minutes.  
5. A scheduled Azure Logic App deletes old files after 1 day.

---

## Repository Layout
<pre> ShareSafely/
├── WebApp/                        # Main C# ASP.NET Core web app
│   ├── Controllers/
│   │   └── HomeController.cs      # Upload logic, SAS link generation
│   ├── Views/
│   ├── wwwroot/
│   └── appsettings.json          # (Optional) If using any local config
│
├── LogicApp/                      # Logic App files for cleanup
│   └── DeleteOldUploads.json     # Exported Logic App definition
│
├── .gitignore
├── README.md
└── azure-publish-settings.json   # (Optional, if saved from Visual Studio)
 </pre>

## Set Up and Deployment 
  
1. **Clone the repository**
   ```bash
   git clone https://github.com/GB72900/share-safely.git
   cd share-safely
   
2. **Create & Configure Resources in Azure**
- Blob Storage account with a container named uploads

- Azure App Service + publish C# web app using Visual Studio

- Azure Key Vault with secret: BlobStorageConnectionString

- Enable Managed Identity for App Service & grant access in Key Vault

3. **Logic App for Cleanup**
- Import LogicApp/DeleteOldUploads.json into Azure

- Use Managed Identity to connect to your Blob Storage

- Set container path to uploads

- Use recurrence trigger (daily)

- Delete blobs older than 1 day using lastModified

## Usage
- Navigate to the deployed web app

- Upload any file

- You’ll receive a secure download link

- Link expires in 30 minutes

- Behind the scenes, a Logic App cleans up expired blobs daily

## Prerequisites
- Azure Subscription

- Visual Studio with ASP.NET Core tools

- Azure CLI or Portal access

- Permissions to deploy App Services, Key Vault, Blob Storage, Logic Apps

  ## Contributing
Pull requests are welcome! If you'd like to improve or extend the project, feel free to open an issue or reach out to: garett.m.blake@gmail.com

## Demo 
- This demo shows a user securely uploading a file through the ShareSafely web app. A secure, time-limited download link is generated instantly using Azure Blob Storage and SAS token logic.


https://github.com/user-attachments/assets/c9b3cbe9-12f5-403f-9444-a7ea28b8a0e3

