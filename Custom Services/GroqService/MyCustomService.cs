using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.AI;
using Syncfusion.Blazor.AI;
namespace CustomAIService
{
    public class MyCustomService : IChatInferenceService
    {

        private IChatClient _chatClient;
        private readonly ErrorDialogService _errorDialogService;

        public MyCustomService(IChatClient client ,ErrorDialogService errorDialogService)
        {
            this._chatClient = client ?? throw new ArgumentNullException(nameof(client));
            this._errorDialogService = errorDialogService ?? throw new ArgumentNullException(nameof(errorDialogService));
        }

        /// <summary>
        /// Sends the chat parameters to the AI client and returns the response.
        /// Also checks and updates token usage.
        /// </summary>
        /// <param name="options">Chat parameters including messages and settings.</param>
        /// <returns>AI-generated response text.</returns
        public async Task<string> GenerateResponseAsync(ChatParameters options)
        {
            ChatOptions completionRequest = new ChatOptions
            {
                Temperature = options.Temperature ?? 0.5f,
                TopP = options.TopP ?? 1.0f,
                MaxOutputTokens = options.MaxTokens ?? 2000,
                FrequencyPenalty = options.FrequencyPenalty ?? 0.0f,
                PresencePenalty = options.PresencePenalty ?? 0.0f,
                StopSequences = options.StopSequences
            };
            try
            {
                ChatResponse completion = await _chatClient.GetResponseAsync(options.Messages[0].Text, completionRequest);
                string rawResponse = completion.Text.ToString();
                if (rawResponse.Contains("```html") || rawResponse.Contains("```"))
                {
                    rawResponse = rawResponse.Replace("```html", "").Replace("```", "").Trim();
                }
                return rawResponse;
            }
            catch (Exception ex)
            {
                _errorDialogService.DialogMessage = ex.Message; // Set the value
                _errorDialogService.RaiseDialogOpen();
                return "";
            }
        }
    }
}
