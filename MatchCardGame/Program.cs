namespace MatchCardGame
{
    public static class Program
    {
        public static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton(Console.In);
            services.AddSingleton(Console.Out);
            services.AddTransient<IConsoleWrapper, ConsoleWrapper>();
            services.AddTransient<IInputService, InputService>();
            services.AddTransient<ICardService, CardService>();
            services.AddTransient<IMatchConditionService, MatchConditionService>();
            services.AddSingleton<IPlayerService, PlayerService>();
            services.AddSingleton<IDeckService, DeckService>();
            services.AddSingleton<IGameService, GameService>();

            return services.BuildServiceProvider();
        }

        public static void RunProgram(IServiceProvider serviceProvider)
        {
            var gameService = serviceProvider.GetRequiredService<IGameService>();
            gameService.Setup();
            gameService.Play();
        }

        public static void Main()
        {
            var services = ConfigureServices();
            RunProgram(services);
        }
    }
}