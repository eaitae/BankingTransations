using BankingTransations.Constants;
using BankingTransations.Entities;
using BankingTransations.Repositories;
using BankingTransations.Services;
using FluentAssertions;

namespace BankingTransations.Tests;

public class ValidationTests
{
    private readonly IAccountRepository _accountRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountService _accountService;
    private readonly ITransactionService _transactionService;

    public ValidationTests()
    {
        _accountRepository = new AccountRepository();
        _transactionRepository = new TransactionRepository();
        _accountService = new AccountService(_accountRepository);
        _transactionService = new TransactionService(_accountService, _transactionRepository);
    }

    [Fact]
    public void CreateFoodTransaction_WithAmountEqualsFoodBalance_ShouldReturnApprovedTransaction()
    {
        // Arrange
        var amount = 100M;
        var merchant = "supermarket";
        var mcc = MerchantCategoryCodes.Food1;

        // Act
        var account = _accountService.CreateAccount(amount, 0, 0);
        var transaction = _transactionService.CreateTransaction(new Transaction(0, account.AccountId, amount, merchant, mcc));

        // Assert
        transaction.IsApproved.Should().BeTrue();
    }

    [Fact]
    public void CreateMealTransaction_WithAmountEqualsMealBalance_ShouldReturnApprovedTransaction()
    {
        // Arrange
        var amount = 87.75M;
        var merchant = "restaurant";
        var mcc = MerchantCategoryCodes.Meal2;

        // Act
        var account = _accountService.CreateAccount(0, amount, 0);
        var transaction = _transactionService.CreateTransaction(new Transaction(0, account.AccountId, amount, merchant, mcc));

        // Assert
        transaction.IsApproved.Should().BeTrue();
    }

    [Fact]
    public void CreateTransaction_WithInsuficientBalance_ShouldReturnRejectedTransaction()
    {
        // Arrange
        var amount = 100M;
        var balance = 50M;
        var merchant = "clothe store";
        var mcc = 0;

        // Act
        var account = _accountService.CreateAccount(balance, balance, balance);
        var transaction = _transactionService.CreateTransaction(new Transaction(0, account.AccountId, amount, merchant, mcc));

        // Assert
        transaction.IsApproved.Should().BeFalse();
    }

    [Fact]
    public void CreateCashTransaction_WithSuficientBalance_ShouldReturnApprovedTransaction()
    {
        // Arrange
        var amount = 12.34M;
        var balance = 50M;
        var merchant = "tech store";
        var mcc = 0;

        // Act
        var account = _accountService.CreateAccount(balance, balance, balance);
        var transaction = _transactionService.CreateTransaction(new Transaction(0, account.AccountId, amount, merchant, mcc));

        // Assert
        transaction.IsApproved.Should().BeTrue();
    }

    [Fact]
    public void CreateTwoTransactions_WithDifferentAmountAndBalance_ShouldAproveFirstRejectSecond()
    {
        // Arrange
        var firstAmount = 12.34M;
        var secondAmount = 40M;
        var balance = 50M;
        var merchant = "restaurant";
        var mcc = MerchantCategoryCodes.Meal1;

        // Act
        var account = _accountService.CreateAccount(0, balance, 0);
        var firstTransaction = _transactionService.CreateTransaction(new Transaction(0, account.AccountId, firstAmount, merchant, mcc));
        var secondTransaction = _transactionService.CreateTransaction(new Transaction(0, account.AccountId, secondAmount, merchant, mcc));

        // Assert
        firstTransaction.IsApproved.Should().BeTrue();
        secondTransaction.IsApproved.Should().BeFalse();
    }
}