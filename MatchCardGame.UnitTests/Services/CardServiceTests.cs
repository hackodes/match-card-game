namespace MatchCardGame.UnitTests.Services
{
    [ExcludeFromCodeCoverage]
    public class CardServiceTests
    {
        [Fact]
        public void AddCard_WhenCardAddedToPile_ShouldContainCardInPile()
        {
            // Arrange
            var cardService = new CardService();
            var card = new Card(3, Suit.Hearts);

            // Act
            cardService.AddCard(card);

            // Assert
            cardService.GetPileCount().Should().Be(1);
        }

        [Fact]
        public void ClearPile_WhenPileContainsCards_ShouldClearPile()
        {
            // Arrange
            var cardService = new CardService();
            var card = new Card(3, Suit.Hearts);
            cardService.AddCard(card);

            // Act
            cardService.ClearPile();

            // Assert
            cardService.GetPileCount().Should().Be(0);
        }

        [Fact]
        public void GetPileCount_WhenPileContainsCards_ShouldReturnPileCount()
        {
            // Arrange
            var cardService = new CardService();
            var card = new Card(3, Suit.Hearts);
            cardService.AddCard(card);

            // Act
            var pileCount = cardService.GetPileCount();

            // Assert
            pileCount.Should().Be(1);
        }

        [Fact]
        public void GetPile_WhenPileContainsCards_ShouldReturnPile()
        {
            // Arrange
            var cardService = new CardService();
            var card = new Card(3, Suit.Hearts);
            cardService.AddCard(card);

            // Act
            var pile = cardService.GetPile();

            // Assert
            pile.Should().Contain(card);
        }

        [Fact]
        public void IsMatch_WhenExactMatch_ShouldReturnTrue()
        {
            // Arrange
            var cardService = new CardService();
            var card1 = new Card(3, Suit.Hearts);
            var card2 = new Card(3, Suit.Hearts);

            // Act
            var isMatch = cardService.IsMatch(card1, MatchCondition.ExactMatch, card2);

            // Assert
            isMatch.Should().BeTrue();
        }

        [Fact]
        public void IsMatch_WhenValueMatch_ShouldReturnTrue()
        {
            // Arrange
            var cardService = new CardService();
            var card1 = new Card(3, Suit.Spades);
            var card2 = new Card(3, Suit.Hearts);

            // Act
            var isMatch = cardService.IsMatch(card1, MatchCondition.ValueMatch, card2);

            // Assert
            isMatch.Should().BeTrue();
        }

        [Fact]
        public void IsMatch_WhenSuitMatch_ShouldReturnTrue()
        {
            // Arrange
            var cardService = new CardService();
            var card1 = new Card(8, Suit.Hearts);
            var card2 = new Card(4, Suit.Hearts);

            // Act
            var isMatch = cardService.IsMatch(card1, MatchCondition.SuitMatch, card2);

            // Assert
            isMatch.Should().BeTrue();
        }

        [Fact]
        public void IsMatch_WhenCardsDoNotMatch_ShouldReturnFalse()
        {
            // Arrange
            var cardService = new CardService();
            var card1 = new Card(3, Suit.Hearts);
            var card2 = new Card(4, Suit.Clubs);

            // Act
            var isMatch = cardService.IsMatch(card1, MatchCondition.ExactMatch, card2);

            // Assert
            isMatch.Should().BeFalse();
        }
    }
}
