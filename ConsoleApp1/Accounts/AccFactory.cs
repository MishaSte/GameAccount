namespace GameAccount;

public static class GameAccountFactory
{
    public static GameAccount ChangeAccountType(GameAccount account, GameAccountType newType)
    {
        switch (newType)
        {
            case GameAccountType.Normal:
                return new GameAccount(account.UserName, account.CurrentRating, account.GamesCount, account.AccountType);
            case GameAccountType.WinStreak:
                return new BonusAccClass(account.UserName, account.CurrentRating, account.GamesCount, account.AccountType);
            case GameAccountType.LoseStreak:
                return new LoseStreakGameAcc(account.UserName, account.CurrentRating, account.GamesCount, account.AccountType);

            default:
                throw new ArgumentException("Invalid account type", nameof(newType));
        }
    }
}
