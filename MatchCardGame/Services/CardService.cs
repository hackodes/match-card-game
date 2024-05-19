namespace MatchCardGame.Services
{
    public class CardService : ICardService
    {
        private List<Card> Pile = [];

        public void AddCard(Card card)
        {
            Pile.Add(card);
        }

        public void ClearPile()
        {
            Pile.Clear();
        }

        public int GetPileCount()
        {
            return Pile.Count;
        }

        public List<Card> GetPile() 
        { 
            return Pile;
        }

        public bool IsMatch(Card card1, MatchCondition matchCondition, Card card2)
        {
            if (card1.Suit == card2.Suit && matchCondition == MatchCondition.SuitMatch)
            {
                return true;
            } 
            else if (card1.Value == card2.Value && matchCondition == MatchCondition.ValueMatch)
            {
                return true;
            }
            else if ((card1.Suit == card2.Suit) && (card1.Value == card2.Value) && (matchCondition == MatchCondition.ExactMatch))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

