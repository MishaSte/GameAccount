namespace GameAccount
{
    public interface IPlayerRepository
    {
        GameAccount Create(GameAccount player);
        GameAccount Read(int playerId);
        IEnumerable<GameAccount> GetAll();
        IEnumerable<BaseGame> GetGamesForPlayer(GameAccount player);
        GameAccount GetPlayerByName(string playerName);
    }

    public class PlayerRepository : IPlayerRepository
    {
        private readonly DbContext _context;

        public PlayerRepository(DbContext context)
        {
            _context = context;
        }
        private int lastUsedUserId = 0;

        private int GetNextUserId()
        {
            lastUsedUserId++;
            return lastUsedUserId;
        }

        public GameAccount Create(GameAccount player)
        {
            player.UserId = GetNextUserId();
            _context.Players.Add(player);
            return player;
        }

        public GameAccount Read(int playerId)
        {
            return _context.Players.FirstOrDefault(p => p.UserId == playerId);
        }
        public IEnumerable<GameAccount> GetAll()
        {
            return _context.Players;
        }
        public IEnumerable<BaseGame> GetGamesForPlayer(GameAccount player)
        {
            return _context.Games.Where(game => game.Player.UserId == player.UserId);
        }

        public GameAccount GetPlayerByName(string playerName)
        {
            return _context.Players.FirstOrDefault(player => player.UserName == playerName);
        }
    }
}
