using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Popups;

namespace CustomService.Components.Pages
{
    public partial class Home
    {
        [Inject]
        public GeminiService? GeminiService { get; set; }

        private string? DialogText { get; set; }

        public async void OpenDialog()
        {
            DialogText = GeminiService!.DialogMessage;
            int fontSize = 16; // px
            int charWidth = (int)(fontSize * 0.6); // Approximate width per character in px
            int baseWidth = 48; // Common addition to width
            int baseHeight = 176; // Additional height

            int textLength = DialogText?.Length ?? 0;
            int calculatedWidth = (textLength * charWidth) + baseWidth;
            int calculatedHeight = fontSize * 2 + baseHeight; // 2 lines minimum + base

            int minWidth = 400;
            int maxWidth = 600;
            int minHeight = 280;
            int maxHeight = 600;

            string width = $"{Math.Clamp(calculatedWidth, minWidth, maxWidth)}px";
            string height = $"{Math.Clamp(calculatedHeight, minHeight, maxHeight)}px";

            await DialogService.AlertAsync(DialogText, "Custom service exception", new DialogOptions()
            {
                ShowCloseIcon = true,
                Width = width,
                Height = height
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
