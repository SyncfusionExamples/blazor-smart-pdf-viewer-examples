using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Popups;

namespace CustomAIService.Components.Pages
{
    public partial class Home
    {
        [Inject]
        public GroqService? GeminiService { get; set; }

        private string? DialogText { get; set; }

        public async void OpenDialog()
        {
            DialogText = GeminiService!.DialogMessage;
            await DialogService.AlertAsync(DialogText, "Custom service exception", new DialogOptions()
            {
                ShowCloseIcon = true,

            });
        }
        protected override void OnInitialized()
        {
            GeminiService!.OnDialogOpen += OpenDialog;
        }
        public void Dispose()
        {
            GeminiService!.OnDialogOpen -= OpenDialog;
        }
    }
}
