namespace MatchCardGame.UnitTests.Services
{
    [ExcludeFromCodeCoverage]
    public class PlayerServiceTests
    {
        [Fact]
        public void CreatePlayers_WhenTwoPlayersCreated_ShouldReturnTwoPlayers()
        {
            // Arrange
            var playerService = new PlayerService();
            var player1Name = "Player 1";
            var player2Name = "Player 2";

            // Act
            var players = playerService.CreatePlayers(player1Name, player2Name);

            // Assert
            players.Should().HaveCount(2);
            players[0].Name.Should().Be(player1Name);
            players[1].Name.Should().Be(player2Name);
            players[0].Score.Should().Be(0);
            players[1].Score.Should().Be(0);
        }

        [Fact]
        public void DeclareMatch_WhenPileContainsCards_ShouldAddScoreToPlayer()
        {
            // Arrange
            var playerService = new PlayerService();
            var player = new Player("Player 1");
            var pileCount = 5;

            // Act
            playerService.DeclareMatch(player, pileCount);

            // Assert
            player.Score.Should().Be(pileCount);
        }

        [Fact]
        public void GetPlayer_WhenPlayerExists_ShouldReturnPlayer()
        {
            // Arrange
            var playerService = new PlayerService();
            var player1Name = "Player 1";
            var player2Name = "Player 2";
            var players = playerService.CreatePlayers(player1Name, player2Name);

            // Act
            var player = playerService.GetPlayer(0);

            // Assert
            player.Should().Be(players[0]);
        }

        [Fact]
        public void GetPlayers_WhenPlayersExist_ShouldReturnPlayers()
        {
            // Arrange
            var playerService = new PlayerService();
            var player1Name = "Player 1";
            var player2Name = "Player 2";
            var players = playerService.CreatePlayers(player1Name, player2Name);

            // Act
            var result = playerService.GetPlayers();

            // Assert
            result.Should().BeEquivalentTo(players);
        }
    }
}
