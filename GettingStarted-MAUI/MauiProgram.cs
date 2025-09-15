using Azure.AI.OpenAI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Logging;
using Syncfusion.Blazor;
using Syncfusion.Blazor.AI;
using System.ClientModel;
using Microsoft.Maui.Storage; // Required for FileSystem
using System.IO;
using System.Threading.Tasks;
using OpenAI.Models;

namespace MauiApp2
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

            string azureOpenAiKey = "Your-API-Key";
            string azureOpenAiEndpoint = "Your-End-Point";
            string OpenAiModel = "Your-Model-Name";

            AzureOpenAIClient azureOpenAIClient = new AzureOpenAIClient(new Uri(azureOpenAiEndpoint), new ApiKeyCredential(azureOpenAiKey));
            IChatClient azureOpenAiChatClient = azureOpenAIClient.GetChatClient(OpenAiModel).AsIChatClient();
            builder.Services.AddChatClient(azureOpenAiChatClient);

            builder.Services.AddSingleton<IChatInferenceService, SyncfusionAIService>();
            builder.Services.AddSyncfusionBlazor();

            // Ensure model.onnx is available before app starts
            //Task.Run(async () => await EnsureModelExistsAsync()).Wait();

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
