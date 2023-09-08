using BankingTransations.Entities;

namespace BankingTransations.Services;

public interface ITransactionService
{
    Transaction CreateTransaction(Transaction transaction);
    IEnumerable<Transaction> GetTransactionsByMerchant(string merchant);
    IEnumerable<Transaction> GetTransactionsByAccountId(int accountId);
}
