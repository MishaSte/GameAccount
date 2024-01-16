namespace GameAccount
{
    public class GameFactory
    {
        public BaseGame CreateGame(GameType gameType, GameAccount player, GameAccount opponent, GameResult result)
        {
            switch (gameType)
            {
                case GameType.Normal:
                    return new NormalGame(player, opponent, result);
                case GameType.Ranked:
                    return new RankedGame(player, opponent, result);
                default:
                    throw new ArgumentException("Такого типу гри не існує");
            }
        }
    }
}
