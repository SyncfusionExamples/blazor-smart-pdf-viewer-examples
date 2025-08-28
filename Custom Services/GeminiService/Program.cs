using CustomService.Components;
using CustomService;
using Syncfusion.Blazor;
using Syncfusion.Blazor.AI;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.  
builder.Services.AddRazorComponents()
   .AddInteractiveServerComponents();

builder.Services.AddSignalR(o => { o.MaximumReceiveMessageSize = 102400000; });

builder.Services.AddMemoryCache();

builder.Services.AddSyncfusionBlazor();

builder.Services.AddSingleton<GeminiService>();
builder.Services.AddSingleton<IChatInferenceService, CustomAIService>();

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



