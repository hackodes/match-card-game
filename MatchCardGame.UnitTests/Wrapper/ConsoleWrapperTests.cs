namespace MatchCardGame.UnitTests.Wrapper
{
    [ExcludeFromCodeCoverage]
    public class ConsoleWrapperTests
    {
        [Fact]
        public void WriteLine_WhenInputIsPassed_ShouldWriteToConsole()
        {
            // Arrange
            var message = "Welcome to Match Card Game!";
            var (stringReader, stringWriter) = SetupTestEntities(message);
            var consoleWrapper = new ConsoleWrapper(stringReader, stringWriter);

            // Act
            consoleWrapper.WriteLine(message);

            // Assert
            stringWriter.ToString().Trim().Should().Be(message);
        }

        [Fact]
        public void ReadLine_WhenInputIsRead_ShouldReturnInputFromConsole()
        {
            // Arrange
            var message = "Welcome to Match Card Game!";
            var (stringReader, stringWriter) = SetupTestEntities(message);
            var consoleWrapper = new ConsoleWrapper(stringReader, stringWriter);

            // Act
            var result = consoleWrapper.ReadLine();

            // Assert
            result.Should().Be(message);
        }

        private static (StringReader, StringWriter) SetupTestEntities(string message = "default")
        {
            var stringReader = new StringReader(message);
            var stringWriter = new StringWriter();

            Console.SetOut(stringWriter);
            Console.SetIn(stringReader);

            return (stringReader, stringWriter);
        }
    }
}
