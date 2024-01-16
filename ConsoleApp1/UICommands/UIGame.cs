namespace GameAccount
{
    public class UIGame
    {
        private readonly GameService _gameService;
        private readonly IPlayerService _playerService;

        public UIGame(GameService gameService, IPlayerService playerService)
        {
            _gameService = gameService;
            _playerService = playerService;
        }

        public void CreateGame()
        {
            while (true)
            {
                GameType selectedGameType;
                var allGameAccounts = _playerService.GetAllPlayers();
                var allGameAccountsCount = allGameAccounts.Count();

                if (allGameAccountsCount == 0)
                {
                    Console.WriteLine("\nYou do not have any created accounts. You must create at least two accounts before creating a game");
                    break;
                }

                if (allGameAccountsCount == 1)
                {
                    Console.WriteLine("\nYou need to create at least two game accounts to create game/n\n ");
                    break;
                }
                else
                {
                    Console.WriteLine("\nChoose what type of game you want: \n1. Normal\n2. Ranked");
                    var gameTypeInput = Console.ReadLine();
                    selectedGameType = GameType.Normal;
                    if (int.TryParse(gameTypeInput, out int gameTypeValue))
                    {
                        switch (gameTypeValue)
                        {
                            case 1:
                                selectedGameType = GameType.Normal;
                                break;
                            case 2:
                                selectedGameType = GameType.Ranked;
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Normal game (1) will be automatically selected.");
                        selectedGameType = GameType.Normal;
                    }
                }

                Console.Write("\nThe list of created game accounts: \n");
                foreach (var gameAccount in allGameAccounts)
                    Console.WriteLine($"{gameAccount.UserId}: {gameAccount.UserName} with {gameAccount.AccountType} account type, Rating: {gameAccount.CurrentRating}");

                Console.WriteLine("\nPlease choose the WINNER from the list by account id");
                var winnerAccountId = Console.ReadLine();

                if (int.TryParse(winnerAccountId, out int parsedWinnerAccountId))
                {
                    var winnerGameAccount = allGameAccounts.FirstOrDefault(account => account.UserId == parsedWinnerAccountId);

                    if (winnerGameAccount != null)
                    {
                    }
                    else
                    {
                        Console.WriteLine("Player with the specified ID does not exist.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid player ID.");
                }

                try
                {
                    parsedWinnerAccountId = int.Parse(winnerAccountId ?? "1");
                    var winnerGameAccount = _playerService.GetPlayerById(parsedWinnerAccountId);

                    Console.WriteLine("\nPlease choose the LOSER from the list by account id");
                    var loserAccountId = Console.ReadLine();

                    try
                    {
                        var parsedLoserAccountId = int.Parse(loserAccountId ?? "2");
                        var loserGameAccount = _playerService.GetPlayerById(parsedLoserAccountId);

                        if (parsedWinnerAccountId == parsedLoserAccountId)
                        {
                            Console.WriteLine("Winner and Loser cannot be the same account");
                            return;
                        }

                        winnerGameAccount.WinGame(_gameService.CreateGame(selectedGameType, winnerGameAccount, loserGameAccount, GameResult.Win));
                        loserGameAccount.LoseGame(_gameService.CreateGame(selectedGameType, loserGameAccount, winnerGameAccount, GameResult.Lose));

                        Console.WriteLine($"\nGame History and Stats of WINNER game account - {winnerGameAccount.UserName} with {winnerGameAccount.AccountType}:");
                        Console.WriteLine(_playerService.GetPlayerById(winnerGameAccount.UserId).GetStats());

                        Console.WriteLine($"\nGame History and Stats of LOSER game account - {loserGameAccount.UserName} with {loserGameAccount.AccountType}:");
                        Console.WriteLine(_playerService.GetPlayerById(loserGameAccount.UserId).GetStats());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("\nYou have entered an invalid number");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nYou have entered an invalid number");
                }

                break;
            }
        }
        public void DisplayAllGames()
        {
            var games = _gameService.GetAllGames();

            if (games.Any())
            {
                Console.WriteLine("List of all games:\n");
                foreach (var game in games)
                {
                    string winnerName = game.Result == GameResult.Win ? game.Player.UserName : game.Opponent.UserName;
                    string loserName = game.Result == GameResult.Win ? game.Opponent.UserName : game.Player.UserName;
                    int winnerRating = game.Result == GameResult.Win ? game.Player.CurrentRating : game.Opponent.CurrentRating;
                    int loserRating = game.Result == GameResult.Win ? game.Opponent.CurrentRating : game.Player.CurrentRating;

                    Console.WriteLine($"| {game.Player.UserName} (Current Rating: {game.Player.CurrentRating})\t| VS | {game.Opponent.UserName} (Current Rating: {game.Opponent.CurrentRating})\t| WINNER: {winnerName}\t| Loser: {loserName}\t|\t\n");
                }
            }
            else
            {
                Console.WriteLine("No games found in the database.");
            }
        }
    }
}
