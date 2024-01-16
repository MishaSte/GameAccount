namespace GameAccount
{
    public interface IGameService
    {
        public BaseGame CreateGame(GameType gameType, GameAccount Player, GameAccount OpponentName, GameResult resultt);
        List<BaseGame> GetAllGames();
    }
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public BaseGame CreateGame(GameType gameType, GameAccount Player, GameAccount OpponentName, GameResult result)
        {
            return _gameRepository.Create(gameType, Player, OpponentName, result);

        }
        public List<BaseGame> GetAllGames()
        {
            return _gameRepository.GetAllGames();
        }
    }
}
