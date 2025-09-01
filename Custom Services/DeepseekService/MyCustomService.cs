using Syncfusion.Blazor.AI;

namespace CustomAIService
{
    public class MyCustomService : IChatInferenceService
    {
        private readonly DeepSeekAIService _DeepSeekService;

        public MyCustomService(DeepSeekAIService DeepSeekService)
        {
            _DeepSeekService = DeepSeekService;
        }

        public Task<string> GenerateResponseAsync(ChatParameters options)
        {
            return _DeepSeekService.CompleteAsync(options.Messages);
        }
    }
}
