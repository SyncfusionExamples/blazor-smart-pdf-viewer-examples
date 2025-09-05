using CustomService;
using CustomService.Components;
using Microsoft.Extensions.AI;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Syncfusion.Blazor;
using Syncfusion.Blazor.AI;
using Syncfusion.Blazor.Popups;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.  
builder.Services.AddRazorComponents()
   .AddInteractiveServerComponents();

builder.Services.AddSignalR(o => { o.MaximumReceiveMessageSize = 102400000; });

builder.Services.AddMemoryCache();

builder.Services.AddSyncfusionBlazor();

builder.Services.AddScoped<ErrorDialogService>();
builder.Services.AddScoped<SfDialogService>();

string geminiApiKey = "Your API Key"
    ?? throw new InvalidOperationException("GEMINI_API_KEY environment variable is not set.");
string geminiAiModel = "Your Model Name";
#pragma warning disable SKEXP0070
var kernelBuilder = Kernel
    .CreateBuilder()
    .AddGoogleAIGeminiChatCompletion(geminiAiModel, geminiApiKey);
Kernel kernel = kernelBuilder.Build();
#pragma warning disable SKEXP0001
IChatClient geminiChatClient = kernel.GetRequiredService<IChatCompletionService>().AsChatClient();
builder.Services.AddChatClient(geminiChatClient);
builder.Services.AddScoped<IChatInferenceService, CustomAIService>(sp =>
{
    ErrorDialogService errorDialogService = sp.GetRequiredService<ErrorDialogService>();
    return new CustomAIService(geminiChatClient,errorDialogService);
});


var app = builder.Build();

// Configure the HTTP request pipeline.  
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();



