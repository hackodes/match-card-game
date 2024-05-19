namespace MatchCardGame.Services
{
    public class PlayerService : IPlayerService
    {
        private Player[] Players = new Player[2];

        public Player[] CreatePlayers(string player1Name, string player2Name)
        {
            Players[0] = new Player(player1Name);
            Players[1] = new Player(player2Name);

            return Players;
        }

        public void DeclareMatch(Player player, int pileCount)
        {
            player.DeclareMatch(pileCount);
        }

        public Player GetPlayer(int index)
        {
            return Players[index];
        }

        public Player[] GetPlayers()
        {
            return Players;
        }
    }
}
