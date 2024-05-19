namespace MatchCardGame.Services
{
    public class DeckService(IConsoleWrapper consoleWrapper) : IDeckService
    {
        private readonly IConsoleWrapper _consoleWrapper = consoleWrapper;
        private readonly List<Card> cards = [];

        public void CreateDeck(int count)
        {
            for (int i = 0; i < count; i++)
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    for (int value = 1; value <= 13; value++)
                    {
                        cards.Add(new Card(value, suit));
                    }
                }
            }
        }

        public void GetCards()
        {
            foreach (Card card in cards)
            {
                _consoleWrapper.WriteLine(card.ToString());
            }
        }

        public int GetTotalCards()
        {
            return cards.Count;
        }

        public List<Card> GetDeck()
        {
            return cards;
        }

        public void TotalCards()
        {
            _consoleWrapper.WriteLine($"Total cards in the deck: {cards.Count}");
        }

        public void Shuffle()
        {
            Random random = new();
            for (int i = 0; i < cards.Count; i++)
            {
                int j = random.Next(i, cards.Count);
                (cards[j], cards[i]) = (cards[i], cards[j]);
            }
        }

        public Card? DrawCard()
        {
            if (cards.Count == 0)
            {
                return null;
            }
            Card card = cards[0];
            cards.RemoveAt(0);
            return card;
        }
    }
}
