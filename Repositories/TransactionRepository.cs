using BankingTransations.Entities;

namespace BankingTransations.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly List<Transaction> _transactionList = new();
    public Transaction CreateTransaction(Transaction transaction)
    {
        //guarantee that every account has an unique id
        var maxId = 0;
        foreach (var transactionItem in _transactionList)
        {
            if (transactionItem.Id > maxId)
            {
                maxId = transactionItem.Id;
            }
        }
        var newTransaction = transaction with
        {
            Id = maxId + 1
        };

        _transactionList.Add(transaction);
        return newTransaction;
    }

    public IEnumerable<Transaction> GetTransactionsByAccountId(int accountId)
    {
        return _transactionList.FindAll((t) => t.AccountId == accountId);
    }

    public IEnumerable<Transaction> GetTransactionsByMerchant(string merchant)
    {
        return _transactionList.FindAll((t) => t.Merchant == merchant);
    }
}
