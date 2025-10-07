namespace WinFormsBlazorHybridApp;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using Azure.AI.OpenAI;
using Microsoft.Extensions.AI;
using Syncfusion.Blazor;
using Syncfusion.Blazor.AI;
using System.ClientModel;
using WinFormsBlazorHybridApp.Components;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        ServiceCollection services = new ServiceCollection();
        services.AddWindowsFormsBlazorWebView();
        services.AddMemoryCache();
        services.AddSyncfusionBlazor();
        string azureOpenAiKey = "api-key";
        string azureOpenAiEndpoint = "endpoint URL";
        string azureOpenAiModel = "deployment-name";
        AzureOpenAIClient azureOpenAIClient = new AzureOpenAIClient(new Uri(azureOpenAiEndpoint), new ApiKeyCredential(azureOpenAiKey));
        IChatClient azureOpenAiChatClient = azureOpenAIClient.GetChatClient(azureOpenAiModel).AsIChatClient();
        services.AddChatClient(azureOpenAiChatClient);
        services.AddSingleton<IChatInferenceService, SyncfusionAIService>();
        BlazorWebView blazorWebView = new BlazorWebView()
        {
            HostPage = "wwwroot\\index.html",
            Services = services.BuildServiceProvider(),
            Dock = DockStyle.Fill
        };
        blazorWebView.RootComponents.Add<YourRazorFileName>("#app");
        // Replace 'YourRazorFileName' with the actual Razor component class (e.g., Main) in your project's namespace
        this.Controls.Add(blazorWebView);
    }
}
