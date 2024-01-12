namespace GameAccount
{
    public class NormalGame : BaseGame
    {
        public NormalGame(GameAccount player, GameAccount opponent)
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
            return 0;
        }

    }
}
