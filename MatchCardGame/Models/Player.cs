namespace MatchCardGame.Models
{
    public class Player(string name)
    {
        public string Name { get; set; } = name;
        public int Score { get; set; } = 0;

        public void DeclareMatch(int pileCount)
        {
            Score += pileCount;
        }
    }
}
