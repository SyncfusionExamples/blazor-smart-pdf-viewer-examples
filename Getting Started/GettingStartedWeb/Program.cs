using Azure.AI.OpenAI;
using GettingStartedWeb.Components;
using Microsoft.Extensions.AI;
using Syncfusion.Blazor;
using Syncfusion.Blazor.AI;
using System.ClientModel;

namespace GettingStartedWeb;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddSignalR(o => { o.MaximumReceiveMessageSize = 102400000; });

        builder.Services.AddMemoryCache();
        builder.Services.AddSyncfusionBlazor();

        string apiKey = "api-key";
        string deploymentName = "deployment-name";
        string endpoint = "endpoint URL";

        string azureOpenAiKey = apiKey;
        string azureOpenAiEndpoint = endpoint;
        string azureOpenAiModel = deploymentName;
        AzureOpenAIClient azureOpenAIClient = new AzureOpenAIClient(new Uri(azureOpenAiEndpoint), new ApiKeyCredential(azureOpenAiKey));
        IChatClient azureOpenAiChatClient = azureOpenAIClient.GetChatClient(azureOpenAiModel).AsIChatClient();
        builder.Services.AddChatClient(azureOpenAiChatClient);

        builder.Services.AddSingleton<IChatInferenceService, SyncfusionAIService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
