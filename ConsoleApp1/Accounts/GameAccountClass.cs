namespace GameAccount
{
    public class GameAccount
    {
        public string UserName { get; set; }
        public int CurrentRating { get; set; }
        public int UserId { get; internal set; }

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
                Game gameRecord = new Game(baseGame.Opponent.UserName, baseGame.CalculateRating(), true);
                GameHistory.Add(gameRecord);
                GamesCount++;
                WinStreak++;
                LoseStreak = 0;
                if (WinStreak >= 3)
                {
                    GameAccountFactory.ChangeAccountType(this, GameAccountType.WinStreak);
                }
                else if (WinStreak == 0)
                {
                    GameAccountFactory.ChangeAccountType(this, GameAccountType.Normal);
                }
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
                Game gameRecord = new Game(baseGame.Opponent.UserName, baseGame.CalculateRating(), false);
                GameHistory.Add(gameRecord);
                GamesCount++;
                WinStreak = 0;
                LoseStreak++;
                if (LoseStreak >= 3)
                {
                    GameAccountFactory.ChangeAccountType(this, GameAccountType.LoseStreak);
                }
                else if (LoseStreak == 0)
                {
                    GameAccountFactory.ChangeAccountType(this, GameAccountType.Normal);
                }
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
