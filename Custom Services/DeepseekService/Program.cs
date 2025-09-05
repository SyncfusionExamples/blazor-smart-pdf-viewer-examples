using CustomAIService.Components;
using Microsoft.Extensions.AI;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Syncfusion.Blazor;
using Syncfusion.Blazor.AI;
using Syncfusion.Blazor.Popups;

namespace CustomAIService;

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
        builder.Services.AddScoped<ErrorDialogService>();
        builder.Services.AddScoped<SfDialogService>();

        string deepSeekApiKey = "Your API Key" ?? throw new InvalidOperationException("DEEPSEEK_API_KEY environment variable is not set.");
        string deploymentName = "Your Model Name"; // This must match your Azure deployment name
        string endpoint = "https://deepseek-resourceres.services.ai.azure.com/"; // Base endpoint only
        var kernelBuilder = Kernel.CreateBuilder().AddAzureOpenAIChatCompletion(
                deploymentName: deploymentName,
                endpoint: endpoint,
                apiKey: deepSeekApiKey
            );
        Kernel kernel = kernelBuilder.Build();
        IChatClient deepSeekChatClient = kernel.GetRequiredService<IChatCompletionService>().AsChatClient();
        builder.Services.AddChatClient(deepSeekChatClient);
        builder.Services.AddScoped<IChatInferenceService, MyCustomService>(sp =>
        {
            ErrorDialogService errorDialogService = sp.GetRequiredService<ErrorDialogService>();
            return new MyCustomService(deepSeekChatClient,errorDialogService);
        });

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
