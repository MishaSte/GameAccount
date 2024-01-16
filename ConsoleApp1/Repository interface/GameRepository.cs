namespace GameAccount
{
    public interface IGameRepository
    {
        public BaseGame Create(GameType gameType, GameAccount Player, GameAccount OpponentName, GameResult result);
        List<BaseGame> GetAllGames();
    }

    public class GameRepository : IGameRepository
    {
        private readonly DbContext _context;
        private readonly GameFactory _gameFactory = new GameFactory();
        public GameRepository(DbContext context)
        {
            _context = context;
        }

        public BaseGame Create(GameType gameType,GameAccount Player, GameAccount OpponentName, GameResult result)
        {
            var game = _gameFactory.CreateGame(gameType, Player, OpponentName, result);
            game.Result = result;
            _context.Games.Add(game);
            return game;
        }
        public List<BaseGame> GetAllGames()
        {
            return _context.Games.ToList();
        }

    }
}

