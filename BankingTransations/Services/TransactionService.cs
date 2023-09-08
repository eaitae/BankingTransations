using BankingTransations.Entities;
using BankingTransations.Repositories;

namespace BankingTransations.Services;

public class TransactionService : ITransactionService
{

    private readonly IAccountService _accountService;
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(IAccountService accountService, ITransactionRepository transactionRepository)
    {
        _accountService = accountService;
        _transactionRepository = transactionRepository;
    }
    public Transaction CreateTransaction(Transaction transaction)
    {
        try
        {
            _accountService.DeductBalance(transaction.AccountId, transaction.Mcc, transaction.Amount);
            return _transactionRepository.CreateTransaction(transaction with { IsApproved = true });

        }
        catch (InvalidOperationException ex)
        {
            return _transactionRepository.CreateTransaction(transaction with { IsApproved = false, RejectionCause = ex.Message });
        }
    }

    public IEnumerable<Transaction> GetTransactionsByAccountId(int accountId)
    {
        return _transactionRepository.GetTransactionsByAccountId(accountId);
    }

    public IEnumerable<Transaction> GetTransactionsByMerchant(string merchant)
    {
        return _transactionRepository.GetTransactionsByMerchant(merchant);
    }
}
