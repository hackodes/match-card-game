namespace MatchCardGame.Interfaces
{
    public interface IPlayerService
    {
        Player[] CreatePlayers(string player1Name, string player2Name);

        void DeclareMatch(Player player, int pileCount);

        Player GetPlayer(int index);

        Player[] GetPlayers();
    }
}
