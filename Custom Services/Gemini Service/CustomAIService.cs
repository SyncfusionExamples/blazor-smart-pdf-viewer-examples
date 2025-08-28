using Syncfusion.Blazor.AI;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.AI;
using SmartComponents.LocalEmbeddings;

namespace CustomService
{
    public class CustomAIService : IChatInferenceService
    {
        private readonly GeminiService _geminiService;
      
        public CustomAIService(GeminiService geminiService)
        {
            _geminiService = geminiService;
        }

        public Task<string> GenerateResponseAsync(ChatParameters options)
        {
            return _geminiService.CompleteAsync(options.Messages);
        }

    }
}
