using BankingTransations.Entities;

namespace BankingTransations.Repositories;

public interface IAccountRepository
{
    Account CreateAccount(Account account);
    Account? GetAccountById(int accountId);
    Account UpdateAccount(Account account);
}
