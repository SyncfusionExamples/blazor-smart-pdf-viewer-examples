namespace CustomAIService
{
    public class ErrorDialogService
    {
        public event Action OnDialogOpen;

        public string DialogMessage { get; set; }

        internal void RaiseDialogOpen()
        {
            OnDialogOpen?.Invoke();
        }
    }
}
