namespace GameAccount
{
    public class Program
    {
        private static void AddMock(GameService gameService, PlayerService playerService)
        {
            var player1 = new GameAccount("John", 100, 0, GameAccountType.Normal);
            var player2 = new GameAccount("Bernt", 100, 0, GameAccountType.Normal);

            playerService.CreatePlayer(player1);
            playerService.CreatePlayer(player2);

            player1.WinGame(gameService.CreateGame(GameType.Ranked, player1, player2, GameResult.Win));
            player2.WinGame(gameService.CreateGame(GameType.Ranked, player2, player1, GameResult.Win));

            player1.WinGame(gameService.CreateGame(GameType.Ranked, player1, player2, GameResult.Win));
            player2.WinGame(gameService.CreateGame(GameType.Ranked, player2, player1, GameResult.Win));

            player1.WinGame(gameService.CreateGame(GameType.Ranked, player1, player2, GameResult.Win));
            player1.WinGame(gameService.CreateGame(GameType.Ranked, player1, player2, GameResult.Win));
            player2.WinGame(gameService.CreateGame(GameType.Ranked, player2, player1, GameResult.Win));
        }
        static void Main(string[] args)
        {
            var dbContext = new DbContext();
            var playerRepository = new PlayerRepository(dbContext);
            var gameRepository = new GameRepository(dbContext);
            var gameService = new GameService(gameRepository);
            var playerService = new PlayerService(playerRepository);

            var gameUI = new UIGame(gameService, playerService);
            var playerUI = new UIPlayer(playerService);

            AddMock(gameService, playerService);

            Console.WriteLine("\n\nWelcome to the game UI!");

            Dictionary<int, (string commandInfo, Action command)> uiCommands = new Dictionary<int, (string, Action)>
    {
        { 1, ("Create a game account", playerUI.CreateAccount) },
        { 2, ("Play a game", gameUI.CreateGame) },
        { 3, ("Print the list of players", playerUI.DisplayAllPlayers) },
        { 4, ("Print the list of games", gameUI.DisplayAllGames) },
        { 5, ("Exit", () => Environment.Exit(0)) }
    };

            while (true)
            {
                Console.WriteLine("\n-----------------");

                foreach (var (optionToPrint, (commandInfo, _)) in uiCommands)
                    Console.WriteLine($"{optionToPrint}. {commandInfo}");

                Console.Write("\nChoose an option: ");

                var choice = Console.ReadLine();

                if (int.TryParse(choice, out var optionToChoose) && uiCommands.ContainsKey(optionToChoose))
                {
                    uiCommands[optionToChoose].command();
                }
                else
                {
                    Console.WriteLine("\nWrong input. Try something else.");
                }
            }
        }

    }
}







