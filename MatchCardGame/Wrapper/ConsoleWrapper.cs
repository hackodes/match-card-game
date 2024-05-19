namespace MatchCardGame.Wrapper
{
    public class ConsoleWrapper(TextReader textReader, TextWriter textWriter) : IConsoleWrapper
    {
        private readonly TextReader _textReader = textReader;
        private readonly TextWriter _textWriter = textWriter;

        public string? ReadLine()
        {
            return _textReader.ReadLine();
        }

        public void WriteLine(string value)
        {
            _textWriter.WriteLine(value);
        }
    }
}
