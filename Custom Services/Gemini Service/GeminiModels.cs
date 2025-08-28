namespace CustomService
{
    public class GeminiModels
    {
        // Represents a text segment in the API communication
        public class Part
        {
            public string Text { get; set; }
        }

        // Contains an array of text parts
        public class Content
        {
            public Part[] Parts { get; init; } = Array.Empty<Part>();
        }

        // Represents a generated response candidate
        public class Candidate
        {
            public Content Content { get; init; } = new();
        }

        // The main response object from Gemini API
        public class GeminiResponseObject
        {
            public Candidate[] Candidates { get; init; } = Array.Empty<Candidate>();
        }

        // Represents a message in the chat conversation
        public class ResponseContent
        {
            public List<Part> Parts { get; init; }
            public string Role { get; init; }  // "user" or "model"

            public ResponseContent(string text, string role)
            {
                Parts = new List<Part> { new Part { Text = text } };
                Role = role;
            }
        }

        // Configuration for text generation
        public class GenerationConfig
        {
            // Controls randomness (0.0 to 1.0)
            public int Temperature { get; init; } = 0;

            // Limits token consideration (1 to 40)
            public int TopK { get; init; } = 0;

            // Nucleus sampling threshold (0.0 to 1.0)
            public int TopP { get; init; } = 0;

            // Maximum tokens in response
            public int MaxOutputTokens { get; init; } = 2048;

            // Sequences that stop generation
            public List<string> StopSequences { get; init; } = new();
        }

        // Controls content filtering
        public class SafetySetting
        {
            // Harm category to filter
            public string Category { get; init; } = string.Empty;

            // Filtering threshold level
            public string Threshold { get; init; } = string.Empty;
        }

        // Main request parameters for Gemini API
        public class GeminiChatParameters
        {
            // Chat message history
            public List<ResponseContent> Contents { get; init; } = new();

            // Generation settings
            public GenerationConfig GenerationConfig { get; init; } = new();

            // Content safety filters
            public List<SafetySetting> SafetySettings { get; init; } = new();
        }
    }
}
