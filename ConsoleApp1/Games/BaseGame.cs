namespace GameAccount
{
    public abstract class BaseGame
    {
        public GameAccount Player { get; set; }
        public GameAccount Opponent { get; set; }
        public GameResult Result { get; set; }

        protected BaseGame(GameAccount player, GameAccount opponent, GameResult result)
        {
            Player = player ?? throw new ArgumentNullException(nameof(player));
            Opponent = opponent ?? throw new ArgumentNullException(nameof(opponent));
            Result = result;
        }
        public abstract int CalculateRating();
        public abstract string GetOppName();
        public abstract string GetPlayerName();
    }
}
