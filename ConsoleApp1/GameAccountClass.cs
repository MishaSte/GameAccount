namespace GameAccount
{
    public class GameAccount
    {
        public string UserName { get; }
        public int CurrentRating { get; set; }
        public int GamesCount = 0;
        protected int WinStreak = 0;
        protected int LoseStreak = 0;

        protected List<Game> GameHistory = new List<Game>();

        public GameAccount(string name, int rate, int GameAmount) // Конструктор
        {
            UserName = name;
            CurrentRating = rate;
            GamesCount = GameAmount;
        }


        public virtual int WinGame(BaseGame baseGame)
        {
            try
            {
                if (baseGame == null)
                {
                    throw new Exception("Гра повинна існувати");
                }
                string OpponentName = baseGame.GetOppName();
                int rating = baseGame.CalculateRating();
                CurrentRating += rating;
                Game GameWin = new Game(OpponentName, rating, true);
                GameHistory.Add(GameWin);
                GamesCount++;
                WinStreak++;
                LoseStreak = 0;
                return CurrentRating;
            }
            catch (Exception e)
            {
                Console.WriteLine("Warning!\t" + e.Message);
                return CurrentRating;
            }
        }

        public virtual int LoseGame(BaseGame baseGame)
        {
            try
            {
                int rating = baseGame.CalculateRating();
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
                string OpponentName = baseGame.GetOppName();
                Game GameLose = new Game(OpponentName, rating, false);
                GameHistory.Add(GameLose);
                GamesCount++;
                WinStreak = 0;
                LoseStreak++;
                return CurrentRating;
            }
            catch (Exception e)
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
    }
}
