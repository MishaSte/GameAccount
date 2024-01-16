namespace GameAccount
{
    public class RankedGame : BaseGame
    {
        private string opponentName;
        private GameResult result;

        public RankedGame(GameAccount player, GameAccount opponent, GameResult result) : base(player, opponent, result)
        {
        }

        public override string GetOppName()
        {
            return Opponent.UserName;
        }
        public override string GetPlayerName()
        {
            return Player.UserName;
        }

        public override int CalculateRating()
        {
            int DefaultScore = 20;
            int RatingDiffernce = Player.CurrentRating - Opponent.CurrentRating;

            if (RatingDiffernce <= -50)
            {
                DefaultScore += (-RatingDiffernce / 10);
            }
            else if (RatingDiffernce >= 50)
            {
                DefaultScore -= RatingDiffernce / 10;
            }

            return DefaultScore;
        }
    }
}
