namespace MatchCardGame.Interfaces
{
    public interface ICardService
    {
        public void AddCard(Card card);

        public void ClearPile();

        public int GetPileCount();

        public List<Card> GetPile();

        public bool IsMatch(Card card1, MatchCondition matchCondition, Card card2);
    }
}
