using Microsoft.Extensions.AI;
using System.Text.Json;
using System.Text;
using System.Net;
using static CustomService.GeminiModels;


namespace CustomService
{
    public class GeminiService
    {
        // HTTP client configuration for optimal performance
        private static readonly Version _httpVersion = HttpVersion.Version30;
        private static readonly HttpClient HttpClient = new HttpClient(new SocketsHttpHandler
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(30),
            EnableMultipleHttp2Connections = true,
        })
        {
            DefaultRequestVersion = _httpVersion
        };

        // Configuration settings
        private const string ApiKey = "Your API key";
        private const string ModelName = "Your model name";

        // JSON serialization settings
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public GeminiService()
        {
            // Set up authentication headers
            HttpClient.DefaultRequestHeaders.Clear();
            HttpClient.DefaultRequestHeaders.Add("x-goog-api-key", ApiKey);
        }

        // Main method for interacting with Gemini API
        public async Task<string> CompleteAsync(IList<ChatMessage> chatMessages)
        {
            // Construct the API endpoint URL
            var requestUri = $"https://generativelanguage.googleapis.com/v1beta/models/{ModelName}:generateContent";

            // Prepare the request parameters
            var parameters = BuildGeminiChatParameters(chatMessages);
            var payload = new StringContent(
               JsonSerializer.Serialize(parameters, JsonOptions),
               Encoding.UTF8,
               new System.Net.Http.Headers.MediaTypeHeaderValue("application/json")
            );

            try
            {
                // Send request and process response
                using var response = await HttpClient.PostAsync(requestUri, payload);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<GeminiResponseObject>(json, JsonOptions);

                // Extract and return the generated text
                return result?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text
                    ?? "No response from model.";
            }
            catch (Exception ex) when (ex is HttpRequestException or JsonException)
            {
                throw new InvalidOperationException("Gemini API error.", ex);
            }
        }

        private GeminiChatParameters BuildGeminiChatParameters(IList<ChatMessage> messages)
        {
            // Convert chat messages to Gemini's format
            var contents = messages.Select(m => new ResponseContent(
                m.Text,
                m.Role == ChatRole.User ? "user" : "model"
            )).ToList();

            // Configure request parameters including safety settings
            return new GeminiChatParameters
            {
                Contents = contents,
                GenerationConfig = new GenerationConfig
                {
                    MaxOutputTokens = 2000,
                    StopSequences = new() { "END_INSERTION", "NEED_INFO", "END_RESPONSE" }
                },
                SafetySettings = new()
            {
                new() { Category = "HARM_CATEGORY_HARASSMENT", Threshold = "BLOCK_ONLY_HIGH" },
                new() { Category = "HARM_CATEGORY_HATE_SPEECH", Threshold = "BLOCK_ONLY_HIGH" },
                new() { Category = "HARM_CATEGORY_SEXUALLY_EXPLICIT", Threshold = "BLOCK_ONLY_HIGH" },
                new() { Category = "HARM_CATEGORY_DANGEROUS_CONTENT", Threshold = "BLOCK_ONLY_HIGH" }
            }
            };
        }
    }
}
