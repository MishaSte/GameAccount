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
                    Console.WriteLine("\nYou need to create at least two game accounts to create a game\n");
                    break;
                }

                Console.WriteLine("\nChoose what type of game you want: \n1. Normal\n2. Ranked");
                var gameTypeInput = Console.ReadLine();
                selectedGameType = GameType.Normal;

                if (int.TryParse(gameTypeInput, out int gameTypeValue) && gameTypeValue == 2)
                {
                    selectedGameType = GameType.Ranked;
                }

                Console.Write("\nThe list of created game accounts: \n");
                foreach (var gameAccount in allGameAccounts)
                {
                    Console.WriteLine($"{gameAccount.UserId}: {gameAccount.UserName} with {gameAccount.AccountType} account type, Rating: {gameAccount.CurrentRating}");
                }

                Console.WriteLine("\nPlease choose the first player from the list by account id");
                var player1AccountId = Console.ReadLine();

                if (int.TryParse(player1AccountId, out int parsedPlayer1AccountId))
                {
                    var player1GameAccount = allGameAccounts.FirstOrDefault(account => account.UserId == parsedPlayer1AccountId);

                    if (player1GameAccount != null)
                    {
                        Console.WriteLine("\nPlease choose the second player from the list by account id");
                        var player2AccountId = Console.ReadLine();

                        if (int.TryParse(player2AccountId, out int parsedPlayer2AccountId))
                        {
                            var player2GameAccount = allGameAccounts.FirstOrDefault(account => account.UserId == parsedPlayer2AccountId);

                            if (player2GameAccount != null && player1GameAccount.UserId != player2GameAccount.UserId)
                            {
                                var ticTacToeGame = new TicTacToeGame();
                                bool isGameOver = false;

                                Console.WriteLine("\nGame in progress...");
                                ticTacToeGame.DisplayBoard();

                                while (!isGameOver)
                                {
                                    Console.WriteLine($"Player {ticTacToeGame.CurrentPlayer}, enter the number of the square to make your move (1-9): ");
                                    if (int.TryParse(Console.ReadLine(), out int moveNumber) && moveNumber >= 1 && moveNumber <= 9)
                                    {
                                        if (ticTacToeGame.MakeMove(moveNumber))
                                        {
                                            ticTacToeGame.DisplayBoard();
                                            char winner = ticTacToeGame.CheckWinner();
                                            if (winner != '.')
                                            {
                                                Console.WriteLine($"Player {winner} wins!");
                                                isGameOver = true;

                                                var loserAccountId = allGameAccounts.FirstOrDefault(account => account.UserId != parsedPlayer1AccountId && account.UserId != parsedPlayer2AccountId);
                                                if (winner == 'X')
                                                {
                                                    player1GameAccount.WinGame(_gameService.CreateGame(selectedGameType, player1GameAccount, player2GameAccount, GameResult.Win));
                                                    player2GameAccount.LoseGame(_gameService.CreateGame(selectedGameType, player2GameAccount, player1GameAccount, GameResult.Lose));
                                                }
                                                else
                                                {
                                                    player2GameAccount.WinGame(_gameService.CreateGame(selectedGameType, player2GameAccount, player1GameAccount, GameResult.Win));
                                                    player1GameAccount.LoseGame(_gameService.CreateGame(selectedGameType, player1GameAccount, player2GameAccount, GameResult.Lose));
                                                }

                                                Console.WriteLine($"\nGame History and Stats of Player {player1GameAccount.UserName} with {player1GameAccount.AccountType}:");
                                                Console.WriteLine(_playerService.GetPlayerById(player1GameAccount.UserId).GetStats());

                                                Console.WriteLine($"\nGame History and Stats of Player {player2GameAccount.UserName} with {player2GameAccount.AccountType}:");
                                                Console.WriteLine(_playerService.GetPlayerById(player2GameAccount.UserId).GetStats());
                                            }
                                            else if (ticTacToeGame.IsBoardFull())
                                            {
                                                Console.WriteLine("It's a draw!");
                                                isGameOver = true;
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid move. Try again.");
                                        }
                                    }
                                }
                            }
                        }
                    }
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
