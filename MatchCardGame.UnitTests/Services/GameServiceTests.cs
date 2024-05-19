namespace MatchCardGame.UnitTests.Services
{
    [ExcludeFromCodeCoverage]
    public class GameServiceTests
    {
        [Fact]
        public void Setup_WhenSetupInitialised_ShouldBuildGame()
        {
            // Arrange
            var inputServiceMock = new Mock<IInputService>();
            inputServiceMock.SetupSequence(x => x.PromptMessage(It.IsAny<string>()))
                .Returns("2") 
                .Returns("Suit Match"); 

            inputServiceMock.Setup(x => x.PrintMessage(It.IsAny<string>()));

            var playerServiceMock = new Mock<IPlayerService>();
            var deckServiceMock = new Mock<IDeckService>();
            var matchConditionServiceMock = new Mock<IMatchConditionService>();
            var cardServiceMock = new Mock<ICardService>();

            var gameService = new GameService(inputServiceMock.Object, deckServiceMock.Object, playerServiceMock.Object, cardServiceMock.Object,
                                                        matchConditionServiceMock.Object);

            // Act
            gameService.Setup();

            // Assert
            playerServiceMock.Verify(x => x.CreatePlayers("Player 1", "Player 2"), Times.Once);
            inputServiceMock.Verify(x => x.PromptMessage("How many packs of cards to use for the game?"), Times.Once);
            inputServiceMock.Verify(x => x.PromptMessage("Which match condition would you like to use?", new string[] { "Suit Match", "Value Match", "Exact Match" }), Times.Once);
            deckServiceMock.Verify(x => x.CreateDeck(2), Times.Once);
            deckServiceMock.Verify(x => x.Shuffle(), Times.Once);
        }

        [Fact]
        public void Play_WhenPileHasMatch_PlayerShouldDeclareMatch()
        {
            // Arrange
            var playerServiceMock = new Mock<IPlayerService>();
            var players = new[] { new Player("Player 1"), new Player("Player 2") };
            playerServiceMock.Setup(x => x.GetPlayers()).Returns(players);
            playerServiceMock.Setup(x => x.DeclareMatch(It.IsAny<Player>(), It.IsAny<int>()))
                                 .Callback<Player, int>((player, pileCount) =>
                                 {
                                     player.DeclareMatch(pileCount);
                                 });

            var deckServiceMock = new Mock<IDeckService>();
            deckServiceMock.SetupSequence(x => x.GetTotalCards())
                .Returns(4) 
                .Returns(3) 
                .Returns(2) 
                .Returns(1)
                .Returns(0);
            deckServiceMock.Setup(x => x.DrawCard()).Returns(new Card(10, Suit.Hearts));

            var matchConditionServiceMock = new Mock<IMatchConditionService>();
            matchConditionServiceMock.Setup(x => x.GetMatchCondition()).Returns(MatchCondition.ValueMatch);


            var cardServiceMock = new Mock<ICardService>();
            var inputServiceMock = new Mock<IInputService>();

            var gameService = new GameService(inputServiceMock.Object, deckServiceMock.Object, playerServiceMock.Object, cardServiceMock.Object, matchConditionServiceMock.Object);

            // Mock the pile to have at least 2 cards
            var pile = new List<Card> { new Card(10, Suit.Hearts), new Card(10, Suit.Clubs) };
            cardServiceMock.Setup(x => x.AddCard(It.IsAny<Card>()));
            cardServiceMock.Setup(x => x.GetPileCount()).Returns(pile.Count);
            cardServiceMock.Setup(x => x.GetPile()).Returns(pile);

            // Set up match condition to return true
            cardServiceMock.Setup(x => x.IsMatch(pile[0], MatchCondition.ValueMatch, pile[1])).Returns(true);

            // Act
            gameService.Play();

            // Assert
            deckServiceMock.Verify(x => x.GetTotalCards(), Times.Exactly(5));
            deckServiceMock.Verify(x => x.DrawCard(), Times.Exactly(8));
            inputServiceMock.Verify(x => x.PrintMessage(It.Is<string>(x => x.Contains("Pile 1 (Last card): " + pile[pile.Count - 1]))), Times.Exactly(8));
            inputServiceMock.Verify(x => x.PrintMessage(It.Is<string>(x => x.Contains("Pile 2 (Second last card): " + pile[pile.Count - 2]))), Times.Exactly(8));
            inputServiceMock.Verify(x => x.PrintMessage("Player 1 declared match!"), Times.AtLeastOnce);
        }

        [Fact]
        public void Play_WhenDrawCardReturnsNull_ShouldEndGame()
        {
            // Arrange
            var playerServiceMock = new Mock<IPlayerService>();
            var players = new[] { new Player("Player 1"), new Player("Player 2") };
            playerServiceMock.Setup(x => x.GetPlayers()).Returns(players);

            var deckServiceMock = new Mock<IDeckService>();
            deckServiceMock.SetupSequence(x => x.GetTotalCards())
                .Returns(2) 
                .Returns(1) 
                .Returns(0); 

            var matchConditionServiceMock = new Mock<IMatchConditionService>();
            var cardServiceMock = new Mock<ICardService>();
            var inputServiceMock = new Mock<IInputService>();

            var gameService = new GameService(inputServiceMock.Object, deckServiceMock.Object, playerServiceMock.Object, cardServiceMock.Object, matchConditionServiceMock.Object);

            // Mock the pile to have at least 2 cards
            var pile = new List<Card> { new Card(10, Suit.Hearts), new Card(10, Suit.Clubs) };
            cardServiceMock.Setup(x => x.GetPileCount()).Returns(pile.Count);
            cardServiceMock.Setup(x => x.GetPile()).Returns(pile);
            cardServiceMock.Setup(x => x.IsMatch(pile[0], MatchCondition.SuitMatch, pile[1])).Returns(true);

            deckServiceMock.SetupSequence(deckServiceMock => deckServiceMock.DrawCard())
                .Returns(new Card(10, Suit.Hearts))
                .Returns((Card?)null);

            // Set up match condition to return true
            cardServiceMock.Setup(x => x.IsMatch(pile[0], MatchCondition.SuitMatch, pile[1])).Returns(true);

            // Act
            gameService.Play();

            // Assert
            deckServiceMock.Verify(x => x.GetTotalCards(), Times.Exactly(3));
            deckServiceMock.Verify(x => x.DrawCard(), Times.Exactly(4));
            inputServiceMock.Verify(x => x.PrintMessage("No more cards in the deck."), Times.AtLeastOnce());
        }
    }
}
