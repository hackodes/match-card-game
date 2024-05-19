namespace MatchCardGame.Interfaces
{
    public interface IInputService
    {
        void PrintMessage(string message);

        string PromptMessage(string message);

        string PromptMessage(string message, string[] options);
    }
}
