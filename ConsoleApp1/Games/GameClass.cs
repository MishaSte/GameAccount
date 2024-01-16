namespace GameAccount
{
    public class Game
    {
        public string OpponentName { get; set; }
        public string PlayerName { get; set; }
        public int Rating { get; set; } // рейтинг на який грають
        public bool GameResult { get; set; } // Перевірка результату гри
        public int GameId { get; internal set; }
        public GameType GameType { get; set; }

        public Game(string opponentName, int rating, bool gameresult) // Конструктор класу Game
        {
            OpponentName = opponentName;
            Rating = rating;
            GameResult = gameresult;
        }
    }
}