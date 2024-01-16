namespace GameAccount
{
    public class DbContext
    {
        public List<GameAccount> Players { get; set; }
        public List<BaseGame> Games { get; set; }
        public DbContext()
        {
            Players = new List<GameAccount>();
            Games = new List<BaseGame>();
        }
    }
}
