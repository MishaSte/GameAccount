namespace GameAccount
{
    public interface IPlayerService
    {
        GameAccount CreatePlayer(GameAccount player);
        GameAccount GetPlayerById(int playerId);
        IEnumerable<GameAccount> GetAllPlayers();
        GameAccount GetPlayerByName(string playerName);
    }

    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }
        public GameAccount CreatePlayer(GameAccount player)
        {
            return _playerRepository.Create(player);
        }
        public GameAccount GetPlayerById(int playerId)
        {
            return _playerRepository.Read(playerId);
        }
        public IEnumerable<GameAccount> GetAllPlayers()
        {
            return _playerRepository.GetAll();
        }
        public IEnumerable<BaseGame> GetGamesForPlayer(GameAccount player)
        {
            return _playerRepository.GetGamesForPlayer(player);
        }
        public GameAccount GetPlayerByName(string playerName)
        {
            return _playerRepository.GetPlayerByName(playerName);
        }
    }
}
