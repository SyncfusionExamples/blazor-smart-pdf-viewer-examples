using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.AI;

namespace CustomAIService
{
    public class GroqService
    {
        public event Action OnDialogOpen;
        public string DialogMessage { get; private set; }
        private void RaiseDialogOpen()
        {
            OnDialogOpen?.Invoke();
        }
        private const string ApiKey = "Your API key";
        private const string ModelName = "Your Model Name";
        private const string Endpoint = "https://api.groq.com/openai/v1/chat/completions";

        private static readonly HttpClient HttpClient = new(new SocketsHttpHandler
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(30),
            EnableMultipleHttp2Connections = true,
        })
        {
            DefaultRequestVersion = HttpVersion.Version30
        };

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public GroqService()
        {
            if (!HttpClient.DefaultRequestHeaders.Contains("Authorization"))
            {
                HttpClient.DefaultRequestHeaders.Clear();
                HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiKey}");
            }
        }

        public async Task<string> CompleteAsync(IList<ChatMessage> chatMessages)
        {
            var requestPayload = new GroqChatParameters
            {
                Model = ModelName,
                Messages = chatMessages.Select(m => new Message
                {
                    Role = m.Role == ChatRole.User ? "user" : "assistant",
                    Content = m.Text
                }).ToList(),
                Stop = new() { "END_INSERTION", "NEED_INFO", "END_RESPONSE" }
            };

            var content = new StringContent(JsonSerializer.Serialize(requestPayload, JsonOptions), Encoding.UTF8, "application/json");

            try
            {
                var response = await HttpClient.PostAsync(Endpoint, content);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var responseObject = JsonSerializer.Deserialize<GroqResponseObject>(responseString, JsonOptions);

                return responseObject?.Choices?.FirstOrDefault()?.Message?.Content ?? "No response from model.";
            }
            catch (Exception ex)
            {
                DialogMessage = ex.Message; // Set the value
                RaiseDialogOpen();
                return "";
            }
        }
    }
}
