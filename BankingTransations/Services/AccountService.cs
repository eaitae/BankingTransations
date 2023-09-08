using BankingTransations.Constants;
using BankingTransations.Entities;
using BankingTransations.Repositories;

namespace BankingTransations.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public Account CreateAccount(decimal foodBalance, decimal mealBalance, decimal cashBalance)
    {
        var newAccount = new Account(0, foodBalance, mealBalance, cashBalance);
        return _accountRepository.CreateAccount(newAccount);

    }

    public Account DeductBalance(int accountId, int mcc, decimal amount)
    {
        var targetAccount = _accountRepository.GetAccountById(accountId) ?? throw new InvalidOperationException("Account not found");

        var updatedAccount = mcc switch
        {
            MerchantCategoryCodes.Food1 or MerchantCategoryCodes.Food2 => DeductFoodBalance(amount, targetAccount),
            MerchantCategoryCodes.Meal1 or MerchantCategoryCodes.Meal2 => DeductMealBalance(amount, targetAccount),
            _ => DeductCashBalance(amount, targetAccount)
        };

        return _accountRepository.UpdateAccount(updatedAccount);
    }

    private static Account DeductCashBalance(decimal amount, Account targetAccount)
    {
        if (targetAccount.CashBalance < amount)
        {
            throw new InvalidOperationException("Insufficient cash balance");
        }
        return targetAccount with
        {
            CashBalance = targetAccount.CashBalance - amount
        };
    }

    private static Account DeductMealBalance(decimal amount, Account targetAccount)
    {
        if (targetAccount.MealBalance < amount)
        {
            throw new InvalidOperationException("Insufficient meal balance");
        }
        return targetAccount with
        {
            MealBalance = targetAccount.MealBalance - amount
        };
    }

    private static Account DeductFoodBalance(decimal amount, Account targetAccount)
    {
        if (targetAccount.FoodBalance < amount)
        {
            throw new InvalidOperationException("Insufficient food balance");
        }
        return targetAccount with
        {
            FoodBalance = targetAccount.FoodBalance - amount
        };
    }

    public Account GetAccountById(int accountId)
    {
        return _accountRepository.GetAccountById(accountId) ?? throw new InvalidOperationException("No account with this id was found");
    }
}
