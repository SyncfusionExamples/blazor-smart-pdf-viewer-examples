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
            Task.Run(async () => await EnsureModelExistsAsync()).Wait();
            return builder.Build();
        }
        private static async Task EnsureModelExistsAsync()
        {
            string[] requiredFiles = { "model.onnx", "vocab.txt" };
            string targetDir = Path.Combine(FileSystem.AppDataDirectory, "LocalEmbeddingsModel/default");

            Directory.CreateDirectory(targetDir);

            foreach (var fileName in requiredFiles)
            {
                var targetPath = Path.Combine(targetDir, fileName);
                if (!File.Exists(targetPath))
                {
                    using var assetStream = await FileSystem.OpenAppPackageFileAsync(fileName);
                    using var fileStream = File.Create(targetPath);
                    await assetStream.CopyToAsync(fileStream);
                }
            }
        }
    }
}
