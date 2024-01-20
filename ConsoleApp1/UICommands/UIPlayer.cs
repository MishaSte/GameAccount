using System.Numerics;

namespace GameAccount
{
    public class UIPlayer
    {
        private readonly PlayerService _playerService;

        public UIPlayer(PlayerService playerService)
        {
            _playerService = playerService;
        }

        public void CreateAccount()
        {
            Console.Write("Enter the player's name: ");
            var name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Player name cannot be empty.");
                return;
            }

            Console.WriteLine("Choose the type of game account:");
            Console.WriteLine("1. Normal");
            Console.WriteLine("2. Win streak");
            Console.WriteLine("3. Lose streak");

            var accountTypeInput = Console.ReadLine();
            if (int.TryParse(accountTypeInput, out int accountTypeValue))
            {
                GameAccountType selectedAccountType = GameAccountType.Normal;

                switch (accountTypeValue)
                {
                    case 1:
                        selectedAccountType = GameAccountType.Normal;
                        break;
                    case 2:
                        selectedAccountType = GameAccountType.WinStreak;
                        break;
                    case 3:
                        selectedAccountType = GameAccountType.LoseStreak;
                        break;
                }

                var player = GameAccountFactory.ChangeAccountType(new GameAccount(name, 100, 0, selectedAccountType), selectedAccountType);
                _playerService.CreatePlayer(player);
                Console.WriteLine("Player account created successfully.");
                Console.WriteLine($"Player ID: {player.UserId}");
                Console.WriteLine($"Player Name: {player.UserName}");
                Console.WriteLine($"Account Type: {player.AccountType}");
                Console.WriteLine($"Current Rating: {player.CurrentRating}");
            }
            else
            {
                Console.WriteLine("Invalid input. Standard account will be automatically selected.");
                var player = new GameAccount(name, 100, 0, GameAccountType.Normal);
                _playerService.CreatePlayer(player);
                Console.WriteLine("Player account created successfully.");
                Console.WriteLine($"Player ID: {player.UserId}");
                Console.WriteLine($"Player Name: {player.UserName}");
                Console.WriteLine($"Account Type: {player.AccountType}");
                Console.WriteLine($"Current Rating: {player.CurrentRating}");
            }
        }
        public bool LogIn()
        {
            Console.Write("Enter your username: ");
            var username = Console.ReadLine();

            var player = _playerService.GetPlayerByName(username);

            if (player != null)
            {
                Console.WriteLine($"Welcome, {username}!");
                return true;
            }
            else
            {
                Console.WriteLine("Invalid username. Please try again.");
                return false;
            }
        }

        public void DisplayAllPlayers()
        {
            var players = _playerService.GetAllPlayers();

            if (players.Any())
            {
                Console.WriteLine("List of all players:\n");
                foreach (var player in players)
                {
                    Console.WriteLine($"Player ID: {player.UserId}");
                    Console.WriteLine($"Player Name: {player.UserName}");
                    Console.WriteLine($"Account Type: {player.AccountType}");
                    Console.WriteLine($"Current Rating: {player.CurrentRating}\n");
                }
            }
            else
            {
                Console.WriteLine("No players found in the database.");
            }
        }
        public void DisplayPlayerStats()
        {
            Console.WriteLine("Enter player name or ID:");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int playerId))
            {
                var player = _playerService.GetPlayerById(playerId);

                if (player != null)
                {
                    Console.WriteLine($"Player Name: {player.UserName}");
                    Console.WriteLine($"Player ID: {player.UserId}");
                    Console.WriteLine($"Account Type: {player.AccountType}");
                    Console.WriteLine($"Current Rating: {player.CurrentRating}");

                    Console.WriteLine("Game History:");
                    Console.WriteLine("Opponent Name\tOpponent Rating\tResult");
                    foreach (var game in player.GameHistory)
                    {
                        Console.WriteLine($"{game.OpponentName}\t\t{game.Rating}\t\t{(game.GameResult ? "Win" : "Lose")}");
                    }
                }
                else
                {
                    Console.WriteLine("Player with the specified ID does not exist.");
                }
            }
            else
            {
                var player = _playerService.GetPlayerByName(input);

                if (player != null)
                {
                    Console.WriteLine($"Player Name: {player.UserName}");
                    Console.WriteLine($"Player ID: {player.UserId}");
                    Console.WriteLine($"Account Type: {player.AccountType}");
                    Console.WriteLine($"Current Rating: {player.CurrentRating}");

                    Console.WriteLine("Game History:");
                    Console.WriteLine("Opponent Name\tOpponent Rating\tResult");
                    foreach (var game in player.GameHistory)
                    {
                        Console.WriteLine($"{game.OpponentName}\t\t{game.Rating}\t\t{(game.GameResult ? "Win" : "Lose")}");
                    }
                }
                else
                {
                    Console.WriteLine("Player with the specified name does not exist.");
                }
            }
        }
    }
}

