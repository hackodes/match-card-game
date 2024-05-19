namespace MatchCardGame.Services
{
    public class GameService(IInputService inputService, IDeckService deckService, IPlayerService playerService, ICardService cardService, IMatchConditionService matchConditionService) : IGameService
    {
        private readonly IInputService InputService = inputService;
        private readonly IDeckService DeckService = deckService;
        private readonly IPlayerService PlayerService = playerService;
        private readonly ICardService CardService = cardService;
        private readonly IMatchConditionService MatchConditionService = matchConditionService;

        #region Public Methods

        public void Setup()
        {
            InputService.PrintMessage("Welcome to the Match Card Game!");

            string deckCount = GetDeckCount();
            string matchCondition = GetMatchCondition();

            InputService.PrintMessage("Setting up game with " + deckCount + " packs of cards.");
            InputService.PrintMessage("Setting up game with " + matchCondition + " match condition.");

            PlayerService.CreatePlayers("Player 1", "Player 2");
            MatchConditionService.SetMatchCondition(matchCondition);
            DeckService.CreateDeck(int.Parse(deckCount));
            DeckService.Shuffle();

            InputService.PrintMessage("Total cards in the deck: " + DeckService.GetTotalCards());
            InputService.PrintMessage("Game setup completed");
        }

        public void Play()
        {
            while (DeckService.GetTotalCards() > 0)
            {
                foreach (Player player in PlayerService.GetPlayers())
                {
                    PlayTurn(player);
                }
            }
            PrintLeaderboard();
        }

        #endregion Public Methods

        #region Private Methods

        private string GetDeckCount()
        {
            return InputService.PromptMessage("How many packs of cards to use for the game?");
        }

        private string GetMatchCondition()
        {
            return InputService.PromptMessage("Which match condition would you like to use?", ["Suit Match", "Value Match", "Exact Match"]);
        }

        private void PlayTurn(Player player)
        {
            InputService.PrintMessage("Number of cards in the pile: " + CardService.GetPileCount());

            Card? card = DeckService.DrawCard();
            if (card == null)
            {
                InputService.PrintMessage("No more cards in the deck.");
                return;
            }
            CardService.AddCard(card);

            if (CardService.GetPileCount() >= 2)
            {
                InputService.PrintMessage("Pile 1 (Last card): " + CardService.GetPile()[CardService.GetPileCount() - 1]);
                InputService.PrintMessage("Pile 2 (Second last card): " + CardService.GetPile()[CardService.GetPileCount() - 2]);
                DeclareMatchIfExists(player);
            }
        }

        private void DeclareMatchIfExists(Player player)
        {
            if (CardService.IsMatch(CardService.GetPile()[CardService.GetPileCount() - 2], MatchConditionService.GetMatchCondition(), CardService.GetPile()[CardService.GetPileCount() - 1]))
            {
                Random random = new Random();
                int index = random.Next(PlayerService.GetPlayers().Length);
                player.DeclareMatch(CardService.GetPileCount());

                InputService.PrintMessage($"{player.Name} declared match!");
                InputService.PrintMessage($"{player.Name} won {CardService.GetPileCount()} cards.");
                CardService.ClearPile();
            }
        }

        private void PrintLeaderboard()
        {
            Player winner = PlayerService.GetPlayers().OrderByDescending(x => x.Score).First();

            InputService.PrintMessage("Game Over!");
            InputService.PrintMessage($"{winner.Name} wins the game with {winner.Score} points!");
            InputService.PrintMessage("Leaderboard:");

            foreach (Player player in PlayerService.GetPlayers())
            {
                InputService.PrintMessage($"{player.Name}: {player.Score} points");
            }
        }

        #endregion Private Methods
    }
}

