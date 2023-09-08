using BankingTransations.Entities;

namespace BankingTransations.Services;

public interface IAccountService
{
    Account CreateAccount(decimal foodBalance, decimal mealBalance, decimal cashBalance);
    Account DeductBalance(int accountId, int mcc, decimal amount);
    Account GetAccountById(int accountId);
}
