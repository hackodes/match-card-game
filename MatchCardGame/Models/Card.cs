namespace MatchCardGame.Models
{
    public class Card(int value, Suit suit)
    {
        public Suit Suit { get; set; } = suit;
        public int Value { get; set; } = value;


        public override string ToString()
        {
            switch (Value)
            {
                case 1:
                    return $"Ace of {Suit}";
                case 11:
                    return $"Jack of {Suit}";
                case 12:
                    return $"Queen of {Suit}";
                case 13:
                    return $"King of {Suit}";
                default:
                    return $"{Value} of {Suit}";
            }
        }
    }
}
