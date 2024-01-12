namespace GameAccount
{
    public class Game
    {
        public string OpponentName { get; private set; }
        public int Rating { get; } // рейтинг на який грають
        public bool GameResult { get; } // Перевірка результату гри

        public Game(string opponentName, int rating, bool gameresult) // Конструктор класу Game
        {
            OpponentName = opponentName;
            Rating = rating;
            GameResult = gameresult;
        }
    }
}