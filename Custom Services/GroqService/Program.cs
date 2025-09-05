using CustomAIService.Components;
using Microsoft.Extensions.AI;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel;
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
        string groqApiKey = "Your API Key"
    ?? throw new InvalidOperationException("GROQ_API_KEY environment variable is not set.");
        string groqModel = "Ypur Model Name";
        var kernelBuilder = Kernel.CreateBuilder().AddOpenAIChatCompletion(
                modelId: groqModel,
                endpoint: new Uri("https://api.groq.com/openai/v1"),
                apiKey: groqApiKey);
        Kernel kernel = kernelBuilder.Build();
        IChatClient groqChatClient = kernel.GetRequiredService<IChatCompletionService>().AsChatClient();
        builder.Services.AddChatClient(groqChatClient);
        
        builder.Services.AddScoped<IChatInferenceService, MyCustomService>(sp =>
        {
            ErrorDialogService errorDialogService = sp.GetRequiredService<ErrorDialogService>();
            return new MyCustomService(groqChatClient ,errorDialogService );
        });
      
        builder.Services.AddScoped<SfDialogService>();

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
