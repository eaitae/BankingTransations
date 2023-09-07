using BankingTransations.Entities;

namespace BankingTransations.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly List<Account> _accountList = new();

    public Account CreateAccount(Account account)
    {
        //guarantee that every account has an unique id
        var maxId = 0;
        foreach (var accountItem in _accountList)
        {
            if (accountItem.AccountId > maxId)
            {
                maxId = accountItem.AccountId;
            }

        }
        var newAccount = account with
        {
            AccountId = maxId + 1
        };

        _accountList.Add(newAccount);
        return newAccount;
    }

    public Account? GetAccountById(int accountId)
    {
        var targetAccount = _accountList.Find((account) => account.AccountId == accountId);

        if (targetAccount == null)
        {
            return null;
        }

        return targetAccount;
    }

    public Account UpdateAccount(Account account)
    {
        var targetAccount = _accountList.Find((a) => a.AccountId == account.AccountId) ?? throw new InvalidOperationException("Account not found");

        var updatedAccount = targetAccount with
        {
            FoodBalance = account.FoodBalance,
            CashBalance = account.CashBalance,
            MealBalance = account.MealBalance,
        };
        _accountList.Remove(targetAccount);
        _accountList.Add(updatedAccount);

        return updatedAccount;
    }
}
