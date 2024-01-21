namespace GameAccount
{
    public class Program
    {
        private static bool isLoggedIn = false;

        private static void AddMock(GameService gameService, PlayerService playerService)
        {
            var player1 = new GameAccount("John", 100, 0, GameAccountType.Normal);
            // var player2 = new GameAccount("Bernt", 100, 0, GameAccountType.Normal);

            playerService.CreatePlayer(player1);
            //  playerService.CreatePlayer(player2);

            // player1.WinGame(gameService.CreateGame(GameType.Ranked, player1, player2, GameResult.Win));
            // player2.WinGame(gameService.CreateGame(GameType.Ranked, player2, player1, GameResult.Win));

            // player1.WinGame(gameService.CreateGame(GameType.Ranked, player1, player2, GameResult.Win));
            // player2.WinGame(gameService.CreateGame(GameType.Ranked, player2, player1, GameResult.Win));

            // player1.WinGame(gameService.CreateGame(GameType.Ranked, player1, player2, GameResult.Win));
            // player1.WinGame(gameService.CreateGame(GameType.Ranked, player1, player2, GameResult.Win));
            // player2.WinGame(gameService.CreateGame(GameType.Ranked, player2, player1, GameResult.Win));
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

            Console.WriteLine("\n\tTic-Tac-Toe\t");

            Dictionary<int, (string commandInfo, Action command)> uiCommandsNotLoggedIn = new Dictionary<int, (string, Action)>
            {
                { 1, ("Create game account", playerUI.CreateAccount) },
                { 2, ("Log in", () => isLoggedIn = playerUI.LogIn()) },
                { 3, ("Exit", () => Environment.Exit(0)) }
            };

            Dictionary<int, (string commandInfo, Action command)> uiCommandsLoggedIn = new Dictionary<int, (string, Action)>
            {
                { 1, ("Play game", gameUI.CreateGame) },
                { 2, ("View game history", gameUI.DisplayAllGames) },
                { 3, ("Exit", () => Environment.Exit(0)) }
            };

            Dictionary<int, (string commandInfo, Action command)> currentUICommands = uiCommandsNotLoggedIn;

            while (true)
            {
                Console.WriteLine("|--------------------------|");

                if (isLoggedIn)
                {
                    Console.WriteLine("Logged in.");
                    currentUICommands = uiCommandsLoggedIn;
                }
                else
                {
                    Console.WriteLine("Not logged in.");
                    currentUICommands = uiCommandsNotLoggedIn;
                }

                foreach (var (optionToPrint, (commandInfo, _)) in currentUICommands)
                {
                    Console.WriteLine($"{optionToPrint}. {commandInfo}");
                }

                Console.Write("\nChoose an option: ");

                var choice = Console.ReadLine();

                if (int.TryParse(choice, out var optionToChoose) && currentUICommands.ContainsKey(optionToChoose))
                {
                    currentUICommands[optionToChoose].command();
                }
                else
                {
                    Console.WriteLine("\nWrong input. Try something else.");
                }
            }
        }
    }
}
