namespace GameAccount
{
    public class BonusAccClass : GameAccount
    {
        public BonusAccClass(string name, int rate, int GameAmount) : base(name, rate, GameAmount)
        {
        }
        public override int WinGame(BaseGame baseGame)
        {
            if (WinStreak >= 3)
            {
                int bonus = 5;
                int ratingBonus = baseGame.CalculateRating() + bonus;
                CurrentRating += ratingBonus;
                string OpponentName = baseGame.GetOppName();
                Game GameWin = new Game(OpponentName, ratingBonus, true);
                base.GameHistory.Add(GameWin);
                GamesCount++;
                WinStreak++;
                LoseStreak = 0;
                return ratingBonus;
            }
            else
            {
                return base.WinGame(baseGame);
            }
        }
    }
}
