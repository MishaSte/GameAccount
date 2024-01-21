namespace GameAccount
{
    public class Game
    {
        public string OpponentName { get; set; }
        public string PlayerName { get; set; }
        public int Rating { get; set; }
        public bool GameResult { get; set; }

        public Game(string opponentName, int rating, bool gameresult) 
        {
            OpponentName = opponentName;
            Rating = rating;
            GameResult = gameresult;
        }
    }
}