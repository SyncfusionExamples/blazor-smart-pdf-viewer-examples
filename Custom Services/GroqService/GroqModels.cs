namespace CustomAIService
{
    public class Choice
    {
        public Message Message { get; set; }
    }

    public class Message
    {
        public string Role { get; set; }
        public string Content { get; set; }
    }

    public class GroqChatParameters
    {
        public string Model { get; set; }
        public List<Message> Messages { get; set; }
        public List<string> Stop { get; set; }
    }

    public class GroqResponseObject
    {
        public string Model { get; set; }
        public List<Choice> Choices { get; set; }
    }
}
