using Syncfusion.Blazor.AI;
namespace CustomAIService
{
    public class MyCustomService : IChatInferenceService
    {
        public GroqService _groqServices;
        public MyCustomService(GroqService groqServices)
        {
            _groqServices = groqServices;
        }
        public Task<string> GenerateResponseAsync(ChatParameters options)
        {
            return _groqServices.CompleteAsync(options.Messages);
            throw new NotImplementedException();
        }
    }
}
