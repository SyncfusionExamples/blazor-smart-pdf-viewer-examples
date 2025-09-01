namespace CustomAIService
{
    public class DeepSeekMessage
    {
        public string Role { get; set; }
        public string Content { get; set; }
    }

    public class DeepSeekChatRequest
    {
        public string Model { get; set; }
        public float Temperature { get; set; }
        public List<DeepSeekMessage> Messages { get; set; }
    }

    public class DeepSeekChatResponse
    {
        public List<DeepSeekChoice> Choices { get; set; }
    }

    public class DeepSeekChoice
    {
        public DeepSeekMessage Message { get; set; }
    }
}
