namespace MatchCardGame.UnitTests.Services
{
    [ExcludeFromCodeCoverage]
    public class InputServiceTests 
    {
        [Fact]
        public void PrintMessage_WhenMessageIsPassed_ShouldPrintMessage()
        {
            // Arrange
            var message = "Test message";
            var consoleWrapperMock = new Mock<IConsoleWrapper>();
            var inputService = new InputService(consoleWrapperMock.Object);

            // Act
            inputService.PrintMessage(message);

            // Assert
            consoleWrapperMock.Verify(x => x.WriteLine(message), Times.Once);
        }

        [Fact]
        public void PromptMessage_WhenValidInputIsPassed_ShouldReturnInput()
        {
            // Arrange
            var message = "How many packs of cards to use for the game?";
            var validInput = "2";

            var consoleMock = new Mock<IConsoleWrapper>();
            consoleMock.SetupSequence(x => x.ReadLine())
                       .Returns(validInput);

            var inputService = new InputService(consoleMock.Object);

            // Act
            var result = inputService.PromptMessage(message);

            // Assert
            consoleMock.Verify(x => x.WriteLine(message), Times.Once);
            consoleMock.Verify(x => x.WriteLine("Invalid input. Please enter a valid number."), Times.Never);

            result.Should().Be(validInput);
        }

        [Fact]
        public void PromptMessage_WhenNonNumericInputIsPassed_ShouldRetryUntilValidInputIsPassed()
        {
            // Arrange
            var message = "How many packs of cards to use for the game?";
            var invalidInput = "invalid";
            var validInput = "2";

            var consoleMock = new Mock<IConsoleWrapper>();
            consoleMock.SetupSequence(x => x.ReadLine())
                       .Returns(invalidInput)  
                       .Returns(validInput);    

            var inputService = new InputService(consoleMock.Object);

            // Act
            var result = inputService.PromptMessage(message);

            // Assert
            consoleMock.Verify(x => x.WriteLine(message), Times.Exactly(2)); 
            consoleMock.Verify(x => x.WriteLine("Invalid input. Please enter a valid number."), Times.Once);
            
            result.Should().Be(validInput);
        }

        [Fact]
        public void PromptMessageWithOptions_WhenUserProvidesInputOption_ShouldReturnInputOption()
        {
            // Arrange
            var message = "Which match condition would you like to use?";
            var inputOptions = new string[] { "Suit Match", "Value Match", "Exact Match" };
            var selectedInputIndex = 0; 
            var selectedInput = inputOptions[selectedInputIndex];

            var consoleMock = new Mock<IConsoleWrapper>();
            consoleMock.Setup(x => x.ReadLine())
                       .Returns((selectedInputIndex + 1).ToString()); 

            var inputService = new InputService(consoleMock.Object);

            // Act
            var result = inputService.PromptMessage(message, inputOptions);

            // Assert
            consoleMock.Verify(x => x.WriteLine(message), Times.Once);
            result.Should().Be(selectedInput);
        }

        [Fact]
        public void PromptMessageWithOptions_WhenInvalidInputIsProvided_ShouldRetryUntilValidInputIsProvided()
        {
            // Arrange
            var message = "Which match condition would you like to use?";
            var inputOptions = new string[] { "Suit Match", "Value Match", "Exact Match" };
            var invalidInput = "invalid";
            var selectedInputIndex = 1; 
            var selectedInput = inputOptions[selectedInputIndex];

            var consoleMock = new Mock<IConsoleWrapper>();
            consoleMock.SetupSequence(x => x.ReadLine())
                       .Returns(invalidInput)
                       .Returns((selectedInputIndex + 1).ToString()); 

            var inputService = new InputService(consoleMock.Object);

            // Act
            var result = inputService.PromptMessage(message, inputOptions);

            // Assert
            consoleMock.Verify(x => x.WriteLine(message), Times.Exactly(2)); 
            consoleMock.Verify(x => x.WriteLine("Invalid input. Please enter a valid number."), Times.Once);
            result.Should().Be(selectedInput);
        }
    }
}
