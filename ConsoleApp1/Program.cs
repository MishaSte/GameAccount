namespace GameAccount
{
    class Program
    {
        static void Main(string[] args)
        {
            GameAccount Player1 = new GameAccount("Edward", 100, 0);
            GameAccount Player2 = new GameAccount("John", 100, 0);
            GameAccount Player3 = new GameAccount("Mark", 100, 0);
            BonusAccClass bonusPlayer = new BonusAccClass("Bob", 100, 0);
            LoseStreakGameAcc loseStreakPlayer = new LoseStreakGameAcc("Loser", 100, 0);

            BaseGame rankedGame = GameFactory.CreateRanked(Player1, Player2);
            BaseGame normalGame = GameFactory.CreateNormal(Player2, Player3);
            BaseGame rankedGame1 = GameFactory.CreateRanked(bonusPlayer, Player3);
            BaseGame rankedGame2 = GameFactory.CreateRanked(bonusPlayer, Player2);
            BaseGame rankedGam3 = GameFactory.CreateRanked(bonusPlayer, Player3);
            BaseGame rankedGam4 = GameFactory.CreateRanked(bonusPlayer, Player1);

            Player1.WinGame(rankedGame);
            Player2.LoseGame(normalGame);
            loseStreakPlayer.LoseGame(rankedGame1);
            loseStreakPlayer.LoseGame(rankedGame2);
            loseStreakPlayer.LoseGame(rankedGam3);
            loseStreakPlayer.LoseGame(rankedGam4);
            bonusPlayer.WinGame(rankedGame1);
            bonusPlayer.WinGame(rankedGame2);
            bonusPlayer.WinGame(rankedGam3);
            bonusPlayer.WinGame(rankedGam4);

            Console.WriteLine(Player1.GetStats());
            Console.WriteLine(Player2.GetStats());
            Console.WriteLine(bonusPlayer.GetStats());
            Console.WriteLine(loseStreakPlayer.GetStats());
        }
    }
}


/*bon.WinGame(Player1.UserName, 20);
bon.WinGame(Player1.UserName, 20);
bon.WinGame(Player1.UserName, 20);
bon.WinGame(Player1.UserName, 20);
bon.LoseGame(Player1.UserName, 20);
bon.WinGame(Player1.UserName, 20);


bon1.LoseGame(Player1.UserName, 20);
bon1.LoseGame(Player2.UserName, 20);
bon1.LoseGame(Player3.UserName, 20);
bon1.LoseGame(Player1.UserName, 20);
bon1.WinGame(Player3.UserName, 20);
bon1.LoseGame(Player1.UserName, 20);


Player1.LoseGame(Player2.UserName, 20);
Player2.WinGame(Player1.UserName, 15);
Player2.WinGame(Player1.UserName, 23);
Player1.WinGame(Player2.UserName, 11);
Player1.WinGame(Player3.UserName, 21);
Player3.WinGame(Player2.UserName, 15);
Player3.LoseGame(Player1.UserName, 10);

Console.WriteLine(Player1.GetStats());
Console.WriteLine(Player2.GetStats());
Console.WriteLine(Player3.GetStats());
Console.WriteLine(bon.GetStats());
Console.WriteLine(bon1.GetStats());*/



