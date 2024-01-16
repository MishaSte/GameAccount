namespace GameAccount
{
    public class Program
    {
        static void Main(string[] args)
        {
            var dbContext = new DbContext();
            var playerRepository = new PlayerRepository(dbContext);
            var playerService = new PlayerService(playerRepository);
            var gameRepository = new GameRepository(dbContext);
            var gameService = new GameService(gameRepository);

            var player1 = playerService.CreatePlayer(new GameAccount("Player1", 100, 0));
            var player2 = playerService.CreatePlayer(new GameAccount("Player2", 100, 0));

            var game = gameService.CreateGame(GameType.Ranked, player2, player1, GameResult.Win);
            var game1 = gameService.CreateGame(GameType.Ranked, player1, player2, GameResult.Lose);
            var game2 = gameService.CreateGame(GameType.Ranked, player1, player2, GameResult.Win);

            player2.WinGame(game);
            player1.LoseGame(game1);
            player1.WinGame(game2);


            Console.WriteLine(player1.GetStats());
            Console.WriteLine(player2.GetStats());

            var gamesForPlayer1 = playerService.GetGamesForPlayer(player1);
            Console.WriteLine("\nGames for Player 1:");
            foreach (var gameItem in gamesForPlayer1)
            {
                string result = gameItem.Result == GameResult.Win ? "Win" : "Lose";
                string opponent = gameItem.Result == GameResult.Win ? gameItem.Opponent.UserName : gameItem.Player.UserName;
                Console.WriteLine($"Game against {opponent}, Result: {result}");
            }

            var gamesForPlayer2 = playerService.GetGamesForPlayer(player2);
            Console.WriteLine("\nGames for Player 2:");
            foreach (var gameItem1 in gamesForPlayer2)
            {
                string result = gameItem1.Result == GameResult.Win ? "Win" : "Lose";
                string opponent = gameItem1.Result == GameResult.Win ? gameItem1.Opponent.UserName : gameItem1.Player.UserName;
                Console.WriteLine($"Game against {opponent}, Result: {result}");
            }

            var allPlayers = playerService.GetAllPlayers();
            Console.WriteLine("\nAll Players:");
            foreach (var player in allPlayers)
            {
                Console.WriteLine($"Player: {player.UserName}, Rating: {player.CurrentRating}, Games Played: {player.GamesCount}");
            }

            var allGames = gameService.GetAllGames();
            Console.WriteLine("\nAll Games:");
            foreach (var gameItem in allGames)
            {
                string resultText = gameItem.Result == GameResult.Win ? "Win" : "Lose";
                string winner = gameItem.Result == GameResult.Win ? gameItem.Player.UserName : gameItem.Opponent.UserName;
                Console.WriteLine($"Game: {gameItem.Player.UserName} vs {gameItem.Opponent.UserName} - Result: {resultText} - Winner: {winner}");
            }
        }
    }
}






