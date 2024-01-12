namespace GameAccount
{
    public class RankedGame : BaseGame
    {
        public RankedGame(GameAccount player, GameAccount opponent)
        {
            Player = player;
            Opponent = opponent;
        }

        public override string GetOppName()
        {
            return Opponent.UserName;
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
