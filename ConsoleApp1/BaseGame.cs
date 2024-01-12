namespace GameAccount
{
    public abstract class BaseGame
    {
        public GameAccount Opponent;
        public GameAccount Player;
        public abstract int CalculateRating();
        public abstract string GetOppName();
    }
}
