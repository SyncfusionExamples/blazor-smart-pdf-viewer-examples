# Syncfusion Blazor Smart PDF Viewer Examples

This repository contains comprehensive examples and samples for the **Syncfusion Blazor Smart PDF Viewer** - an AI-powered PDF viewing and processing component that enhances document handling with intelligent features. The Smart PDF Viewer leverages advanced AI technologies to provide document summarization, smart form filling, and intelligent redaction capabilities.

![Syncfusion Smart PDF Viewer](./Getting%20Started/GettingStarted/wwwroot/images/smart-pdf-viewer.png)

## Key AI-Powered Features in Smart PDF Viewer

The Syncfusion Blazor Smart PDF Viewer incorporates three powerful AI-driven features that revolutionize document processing:

### AI-Powered Document Summarization and Interactive Q&A

Advanced AI capabilities for intelligent document analysis and content understanding:
- Intelligent content extraction and semantic analysis using advanced NLP models
- Context-aware document summarization with key insights highlighting
- Interactive Q&A functionality enabling conversational document exploration
- Multi-language support for global document processing workflows
- Smart content categorization and automated topic identification

![Document Summarization Demo](./Getting%20Started/GettingStarted/wwwroot/images/document-summarizer.gif)

### Intelligent Document Redaction and Privacy Protection
AI-powered redaction system that intelligently identifies and protects sensitive information:
- Automatic detection of PII (Personally Identifiable Information) including SSN, emails, phone numbers
- Custom pattern recognition for domain-specific sensitive data (medical records, financial information)
- Smart context analysis for accurate redaction without over-blocking important content
- Customizable redaction patterns and industry-specific compliance rules
- Batch redaction capabilities for processing multiple documents efficiently

![Smart Redaction Demo](./Getting%20Started/GettingStarted/wwwroot/images/smart-redaction.gif)

### AI-Enhanced Smart Form Filling and Data Extraction
Intelligent form filling system with advanced data extraction and field mapping capabilities:
- Extract relevant information from clipboard content, documents, or structured data sources
- Intelligently map extracted data to appropriate form fields with high accuracy

![Smart Fill Demo](./Getting%20Started/GettingStarted/wwwroot/images/smart-fill.gif)

## Configuration and Setup Instructions

### System Requirements and Prerequisites
- .NET 8.0 or later
- Visual Studio 2022 (v17.4 or later) or Visual Studio Code with C# extension
- An active OpenAI API key or Azure OpenAI service subscription
- Minimum 4GB RAM for optimal AI processing performance
- Internet connection for AI service communication

### Configuring AI Service Credentials

To run AI samples, navigate to the `Program.cs` file in any sample project and replace the following placeholders with your actual credentials:

#### Azure OpenAI Configuration
```csharp
string apiKey = "your-api-key";
string deploymentName = "your-deployment-name";
string endpoint = "your-azure-endpoint-url";
```

Your Azure endpoint URL structure:
`https://{resource_name}.openai.azure.com/`

To use Azure OpenAI, install the [Azure.AI.OpenAI](https://www.nuget.org/packages/Azure.AI.OpenAI) package separately in your Blazor application.

#### OpenAI Configuration
If you are using **OpenAI**, [create an API key](https://help.openai.com/en/articles/4936850-where-do-i-find-my-openai-api-key) and configure as follows:

```csharp
string openAiKey = "your-openai-api-key";
string deploymentName = "gpt-3.5-turbo"; // or gpt-4, gpt-4-turbo
string endpoint = ""; // Leave empty for OpenAI
```

The `deploymentName` should be the [model](https://platform.openai.com/docs/models/) you wish to use (e.g., `gpt-3.5-turbo`, `gpt-4`, `gpt-4-turbo`).

## Quick Start Guide and Setup Instructions

### Step 1: Repository Setup
```bash
git clone https://github.com/syncfusion/blazor-smart-pdf-viewer-examples.git
cd blazor-smart-pdf-viewer-examples
```

### Step 2: Choose and Navigate to a Sample
```bash
cd "Getting Started/GettingStarted"
```

### Step 3: Configure AI Service Credentials
- Open `Program.cs` in your preferred editor
- Replace placeholder values with your actual AI service credentials
- Ensure proper API key format and endpoint configuration
- Save the configuration changes

### Step 4: Build and Run the Sample
```bash
dotnet restore
dotnet build
dotnet run
```

### Step 5: Access the Application
- Open your web browser
- Navigate to `https://localhost:5001` (HTTPS) or `http://localhost:5000` (HTTP)
- Upload a PDF document to test AI features
- Explore Document Summarization, Smart Redaction, and Smart Fill  capabilities
- Interact with the AI assistant for document analysis and Q&A

### Troubleshooting Common Issues
- **API Key Issues**: Verify your API key is valid and has sufficient quota
- **Network Connectivity**: Ensure stable internet connection for AI service calls
- **Port Conflicts**: Use `dotnet run --urls="https://localhost:5555"` for custom ports


## Multi-Platform AI Service Integration and Optimization
The repository provides comprehensive examples for integrating various AI service providers with optimized configurations:

- **Azure OpenAI Services**: Enterprise-grade AI integration with advanced security, compliance features, and scalable deployment options
- **OpenAI Platform**: Direct OpenAI API integration supporting the latest GPT models with custom fine-tuning capabilities
- **Google Gemini AI**: Advanced multimodal AI processing with superior language understanding and document analysis
- **Groq AI Infrastructure**: High-performance AI inference with specialized hardware acceleration for real-time applications


### Official Documentation

This table provides a comprehensive overview of all the Smart PDF Viewer AI features and samples in this repository, along with links to their respective documentation site for detailed implementation guidance and documentation.

<table>
    <thead>
        <tr>
            <th>Category</th>
            <th>Feature</th>
            <th>Description</th>
            <th>Documentation Link</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td><strong>Getting Started</strong></td>
            <td>Basic AI Integration Setup</td>
            <td>Comprehensive introduction to Smart PDF Viewer with foundational AI functionality setup, including OpenAI/Azure OpenAI configuration and basic feature implementation.</td>
            <td><a href="https://help.syncfusion.com/document-processing/pdf/smart-pdf-viewer/blazor/getting-started/web-app">Read More</a></td>
        </tr>
        <tr>
            <td><strong>Document Summarization</strong></td>
            <td>AI Assistant Interface</td>
            <td>Customizable AI assistant interface with panel positioning, conversation history management, and response formatting options for optimal user experience.</td>
            <td><a href="https://help.syncfusion.com/document-processing/pdf/smart-pdf-viewer/blazor/document-summarizer">Read More</a></td>
        </tr>
        <tr>
            <td><strong>Smart Redaction</strong></td>
            <td>AI-Powered PII Detection</td>
            <td>Automatic detection and redaction of personally identifiable information (PII) including SSN, emails, phone numbers with context-aware analysis.</td>
            <td><a href="https://help.syncfusion.com/document-processing/pdf/smart-pdf-viewer/blazor/smart-redaction">Read More</a></td>
        </tr>
        <tr>
            <td><strong>Smart Form Filling</strong></td>
            <td>AI-Enhanced Form Completion</td>
            <td>Intelligent form filling with AI-powered data extraction from clipboard content, supporting various data formats and contextual field suggestions.</td>
            <td><a href="https://help.syncfusion.com/document-processing/pdf/smart-pdf-viewer/blazor/smart-fill">Read More</a></td>
        </tr>
    </tbody>
</table>