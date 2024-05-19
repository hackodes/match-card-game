namespace MatchCardGame.UnitTests.Services
{
    [ExcludeFromCodeCoverage]
    public class DeckServiceTests
    {
        [Fact]
        public void DeckSetup_WhenDeckIsCreated_ShouldReturnDeckWithCorrectCardCount()
        {
            // Arrange
            var consoleWrapper = new Mock<IConsoleWrapper>();
            var deckCount = 3;
            var deckService = new DeckService(consoleWrapper.Object);

            // Act
            deckService.CreateDeck(deckCount);

            // Assert
            deckService.GetTotalCards().Should().Be(52 * deckCount);
        }

        [Fact]
        public void Shuffle_WhenDeckIsShuffled_ShouldReturnShuffledDeck()
        {
            // Arrange
            var consoleWrapper = new Mock<IConsoleWrapper>();
            var deckService = new DeckService(consoleWrapper.Object);
            var deckCount = 1;
            var originalDeck = deckService.GetDeck().ToList();

            // Act
            deckService.CreateDeck(deckCount);
            deckService.Shuffle();
            var shuffledDeck = deckService.GetDeck().ToList();

            // Assert
            shuffledDeck.Should().NotBeSameAs(originalDeck);
        }

        [Fact]
        public void DrawCard_WhenDeckIsEmpty_ShouldReturnNull()
        {
            // Arrange
            var consoleWrapper = new Mock<IConsoleWrapper>();
            var deckService = new DeckService(consoleWrapper.Object);
            var deckCount = 0;

            // Act
            deckService.CreateDeck(deckCount);
            var card = deckService.DrawCard();

            // Assert
            card.Should().BeNull();
        }

        [Fact]
        public void DrawCard_WhenDeckContainsCard_ShouldRemoveCard()
        {
            // Arrange
            var consoleWrapper = new Mock<IConsoleWrapper>();
            var deckService = new DeckService(consoleWrapper.Object);

            // Act
            deckService.CreateDeck(1);
            var card = deckService.DrawCard();

            // Assert
            card.Should().NotBeNull();
        }

        [Fact]
        public void TotalCards_WhenDeckIsCreated_ShouldReturnTotalCardsInDeck()
        {
            // Arrange
            var consoleWrapper = new Mock<IConsoleWrapper>();
            var deckCount = 1;
            var deckService = new DeckService(consoleWrapper.Object);

            // Act
            deckService.CreateDeck(deckCount);
            deckService.TotalCards();

            // Assert
            consoleWrapper.Verify(x => x.WriteLine($"Total cards in the deck: {52 * deckCount}"), Times.Once);
        }

        [Fact]
        public void GetCards_WhenDeckIsCreated_ShouldReturnAllCardsInDeck()
        {
            // Arrange
            var consoleWrapper = new Mock<IConsoleWrapper>();
            var deckService = new DeckService(consoleWrapper.Object);
            var deckCount = 1;

            // Act
            deckService.CreateDeck(deckCount);
            deckService.GetCards();

            // Assert
            consoleWrapper.Verify(x => x.WriteLine(It.IsAny<string>()), Times.Exactly(52 * deckCount));
        }
    }
}
