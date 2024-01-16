namespace GameAccount
{
    public class NormalGame : BaseGame
    {
        private string opponentName;
        private GameResult result;

        public NormalGame(GameAccount player, GameAccount opponent, GameResult result) : base(player, opponent, result)
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
            return 0;
        }

    }
}
