using MatchCardGame.Models;

namespace MatchCardGame.Interfaces
{
    public interface IDeckService
    {
        public void CreateDeck(int count);

        public void Shuffle();

        public Card? DrawCard();

        public void TotalCards();

        public int GetTotalCards();
    }
}
