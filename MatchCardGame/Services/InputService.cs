namespace MatchCardGame.Services
{
    public class InputService(IConsoleWrapper consoleWrapper) : IInputService
    {
        private readonly IConsoleWrapper _consoleWrapper = consoleWrapper;

        public void PrintMessage(string message)
        {
            _consoleWrapper.WriteLine(message);
        }

        public string PromptMessage(string message)
        {
            _consoleWrapper.WriteLine(message);

            var input = _consoleWrapper.ReadLine();

            if (!int.TryParse(input, out _))
            {
                _consoleWrapper.WriteLine("Invalid input. Please enter a valid number.");
                return PromptMessage(message);
            }
            return input;
        }

        public string PromptMessage(string message, string[] options)
        {
            _consoleWrapper.WriteLine(message);

            for (int i = 0; i < options.Length; i++)
            {
                _consoleWrapper.WriteLine($"{i + 1}. {options[i]}");
            }

            var input = _consoleWrapper.ReadLine();

            if (!int.TryParse(input, out int inputNumber) || inputNumber < 1 || inputNumber > options.Length)
            {
                _consoleWrapper.WriteLine("Invalid input. Please enter a valid number.");
                return PromptMessage(message, options);
            }
            return options[inputNumber - 1];
        }
    }
}
