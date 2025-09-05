using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Popups;

namespace CustomAIService.Components.Pages
{
    public partial class Home : IDisposable
    {
        [Inject]
        public ErrorDialogService? ErrorDialogService { get; set; }

        [Inject]
        public SfDialogService? DialogService { get; set; }

        private string? DialogText { get; set; }

        public async void OpenDialog()
        {
            DialogText = ErrorDialogService!.DialogMessage;
            int fontSize = 16; // px
            int charWidth = (int)(fontSize * 0.6); // Approximate width per character in px
            int baseWidth = 48; // Common addition to width
            int baseHeight = 140; // Additional height

            int textLength = DialogText?.Length ?? 0;
            int calculatedWidth = (textLength * charWidth) + baseWidth;
            int calculatedHeight = fontSize * 2 + baseHeight; // 2 lines minimum + base

            int minWidth = 400;
            int maxWidth = 500;

            string width = $"{Math.Clamp(calculatedWidth, minWidth, maxWidth)}px";
            string height = calculatedWidth > maxWidth ? $"{calculatedHeight + 19.2}px" : $"{calculatedHeight}px";

            await DialogService.AlertAsync(DialogText, "Custom service exception", new DialogOptions()
            {
                ShowCloseIcon = true,
                Width = width,
                Height = height
            });
        }

        protected override void OnInitialized()
        {
            ErrorDialogService!.OnDialogOpen += OpenDialog;
        }

        public void Dispose()
        {
            ErrorDialogService!.OnDialogOpen -= OpenDialog;
        }
    }
}
