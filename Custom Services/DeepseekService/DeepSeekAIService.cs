using System.Text;
using System.Text.Json;
using System.Net;
using Microsoft.Extensions.AI;

namespace CustomAIService
{
    public class DeepSeekAIService
    {
        private const string ApiKey = "Your API key";
        private const string ModelName = "Your Model Name";
        private const string Endpoint = "Endpoint";

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

        public DeepSeekAIService()
        {
            if (!HttpClient.DefaultRequestHeaders.Contains("Authorization"))
            {
                HttpClient.DefaultRequestHeaders.Clear();
                HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiKey}");
            }
        }

        public async Task<string> CompleteAsync(IList<ChatMessage> chatMessages)
        {
            DeepSeekChatRequest requestBody = new DeepSeekChatRequest
            {
                Model = ModelName,
                Temperature = 0.7f,
                Messages = chatMessages.Select(m => new DeepSeekMessage
                {
                    Role = m.Role == ChatRole.User ? "user" : "system",
                    Content = m.Text
                }).ToList()
            };


            string json = JsonSerializer.Serialize(requestBody, JsonOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await HttpClient.PostAsync(Endpoint, content);
                response.EnsureSuccessStatusCode();

                string responseString = await response.Content.ReadAsStringAsync();
                DeepSeekChatResponse? responseObject = JsonSerializer.Deserialize<DeepSeekChatResponse>(responseString, JsonOptions);

                return responseObject?.Choices?.FirstOrDefault()?.Message?.Content ?? "No response from DeepSeek.";
            }
            catch (Exception ex) when (ex is HttpRequestException || ex is JsonException)
            {

                throw new InvalidOperationException("Failed to communicate with DeepSeek API.", ex);
            }
        }
    }
}
