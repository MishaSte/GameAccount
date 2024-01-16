namespace GameAccount
{
    public class LoseStreakGameAcc : GameAccount
    {
        public LoseStreakGameAcc(string name, int rate, int GameAmount, GameAccountType accountType) : base(name, rate, GameAmount,accountType)
        {
        }

        public override int LoseGame(BaseGame baseGame)
        {
            if (LoseStreak >= 3)
            {
                int ratingProtection = baseGame.CalculateRating()/2;
                CurrentRating += ratingProtection;
                string OpponentName = baseGame.GetOppName();
                Game GameLose = new Game(OpponentName, ratingProtection, false);
                base.GameHistory.Add(GameLose);
                GamesCount++;
                WinStreak = 0;
                LoseStreak++;
                return ratingProtection;
            }
            else
                return base.LoseGame(baseGame);
        }
    }
}
