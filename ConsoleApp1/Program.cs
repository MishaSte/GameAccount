namespace GameAccount
{
    public class GameAccount 
    {
        public string UserName { get; }
        public int CurrentRating { get; set; }
        public int GamesCount = 0;

        private List<Game> GameHistory = new List<Game>();

        public GameAccount(string name, int rate, int GameAmount) // Конструктор
        {
            UserName = name;
            CurrentRating = rate;
            GamesCount = GameAmount;
        }


        public int WinGame(string OpponentName, int rating)
        {
            try
            {
                if (rating < 0)
                {
                    throw new Exception ("Рейтинг не може бути < 0");
                }

                CurrentRating += rating;
                Game GameWin = new Game(OpponentName, rating, true);
                GameHistory.Add(GameWin);
                GamesCount++;
                return CurrentRating;
            }
            catch (Exception e)
            {
                Console.WriteLine ("Warning!\t" + e.Message);
                return CurrentRating;
            }
        }

        public int LoseGame(string OpponentName, int rating)
        {
            try
            {
                if (rating < 0)
                {
                    throw new Exception("Рейтинг не може бути < 0");
                }

                int minRating = 1;
                if (CurrentRating < minRating)
                {
                    CurrentRating = minRating;
                }
                else
                {
                    CurrentRating -= rating;
                }
                Game GameLose = new Game(OpponentName, rating, false);
                GameHistory.Add(GameLose);
                GamesCount++;
                return CurrentRating;
            }
            catch(Exception e)
            {
                Console.WriteLine("Warning!\t" + e.Message);
                return CurrentRating;
            }
        }

        public string GetStats()
        {
            var stats = new System.Text.StringBuilder();
            stats.AppendLine("OpponentName\tRating\tResult\tGameIndex");
            int GameIndex = 1;
            foreach (var item in GameHistory)
            {
                string result = item.GameResult ? "Win" : "Lose";
                stats.AppendLine($"{item.OpponentName}\t\t{item.Rating.ToString()}\t{result}\tGame {GameIndex}");
                GameIndex++;
            }

            return stats.ToString();
        }


        static void Main(string[] args)
        {
            GameAccount Player1 = new GameAccount("Edward", 100, 0);
            GameAccount Player2 = new GameAccount("John", 100, 0);
            GameAccount Player3 = new GameAccount("Mark", 100, 0);

            Player1.LoseGame(Player2.UserName, 20);
            Player2.WinGame(Player1.UserName, 15);
            Player2.WinGame(Player1.UserName, 23);
            Player1.WinGame(Player2.UserName, 11);
            Player1.WinGame(Player3.UserName, 21);
            Player3.WinGame(Player2.UserName, 15);
            Player3.LoseGame(Player1.UserName, 10);

            Console.WriteLine(Player1.GetStats());
            Console.WriteLine(Player2.GetStats());
            Console.WriteLine(Player3.GetStats());
        }
    }
}
