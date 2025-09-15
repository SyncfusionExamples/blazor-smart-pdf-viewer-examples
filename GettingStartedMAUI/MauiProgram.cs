using Azure.AI.OpenAI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Logging;
using Syncfusion.Blazor;
using Syncfusion.Blazor.AI;
using System.ClientModel;

namespace GettingstartedMAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddMemoryCache();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            string azureOpenAiKey = "Your API Key";
            string azureOpenAiEndpoint = "Your End Point";
            string OpenAiModel = "Your Model name";

            AzureOpenAIClient azureOpenAIClient = new AzureOpenAIClient(new Uri(azureOpenAiEndpoint), new ApiKeyCredential(azureOpenAiKey));
            IChatClient azureOpenAiChatClient = azureOpenAIClient.GetChatClient(OpenAiModel).AsIChatClient();
            builder.Services.AddChatClient(azureOpenAiChatClient);

            builder.Services.AddSingleton<IChatInferenceService, SyncfusionAIService>();
            builder.Services.AddSyncfusionBlazor();

            return builder.Build();
        }

    }
}
