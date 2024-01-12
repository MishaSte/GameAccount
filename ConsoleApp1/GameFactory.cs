namespace GameAccount
{
    public class GameFactory
    {
        public static BaseGame CreateRanked(GameAccount player, GameAccount opponent)
        {
            return new RankedGame(player, opponent);
        }
        public static BaseGame CreateNormal(GameAccount player, GameAccount opponent)
        {
            return new NormalGame(player, opponent);
        }
    }
}
