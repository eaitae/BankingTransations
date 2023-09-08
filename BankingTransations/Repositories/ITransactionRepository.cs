using BankingTransations.Entities;

namespace BankingTransations.Repositories;

public interface ITransactionRepository
{
    Transaction CreateTransaction(Transaction transaction);
    IEnumerable<Transaction> GetTransactionsByMerchant(string merchant);
    IEnumerable<Transaction> GetTransactionsByAccountId(int accountId);
}
