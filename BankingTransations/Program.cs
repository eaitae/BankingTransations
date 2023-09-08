using BankingTransations.Constants;
using BankingTransations.Entities;
using BankingTransations.Repositories;
using BankingTransations.Services;
using System.Globalization;

Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

var accountRepository = new AccountRepository();
var transactionRepository = new TransactionRepository();
var accountService = new AccountService(accountRepository);
var transactionService = new TransactionService(accountService, transactionRepository);

string? chosenOption = string.Empty;
Console.WriteLine("Welcome to our BankingTransactions!");
while (chosenOption != MenuOptions.Cancel)
{

    Console.Write($"""
        What would you like to do?
        {MenuOptions.CreateNewAccount}. Create new account 
        {MenuOptions.MakeATransaction}. Make a transaction 
        {MenuOptions.ConsultTransactionsByAccountId}. Consult transactions by accountId
        {MenuOptions.ConsultTransactionsByMerchant}. Consult transactions by merchant
        {MenuOptions.ConsultAccount}. Consult account
        {MenuOptions.Cancel}. Cancel

        Please, type just the number of the desired option: 
        """);

    chosenOption = Console.ReadLine();

    switch (chosenOption)
    {
        case MenuOptions.CreateNewAccount:
            CreateNewAccount();
            break;
        case MenuOptions.MakeATransaction:
            MakeTransaction();
            break;
        case MenuOptions.ConsultTransactionsByAccountId:
            ConsultTransactionsByAccountId();
            break;
        case MenuOptions.ConsultTransactionsByMerchant:
            ConsultTransactionsByMerchant();
            break;
        case MenuOptions.ConsultAccount:
            ConsultAccount();
            break;
        case MenuOptions.Cancel:
            Console.WriteLine("Thank you for using the BankingTransactions. This application will stop now.");
            break;
        default:
            Console.WriteLine("Option invalid. Please, try again");
            break;
    }

    Console.WriteLine(Environment.NewLine);
}

static string ReadString(string message)
{
    Console.Write(message);
    return Console.ReadLine() ?? string.Empty;
}

static decimal ReadDecimal(string message)
{
    Console.Write(message);
    var input = Console.ReadLine();
    decimal result;
    while (!decimal.TryParse(input, out result))
    {
        Console.WriteLine("The given input is not valid. The value need to be a number. Please, try again.");
        input = Console.ReadLine();
    }
    return result;
}

static int ReadInt(string message)
{
    Console.Write(message);
    var input = Console.ReadLine();
    int result;
    while (!int.TryParse(input, out result))
    {
        Console.WriteLine("The given input is not valid. The value need to be a number. Please, try again.");
        input = Console.ReadLine();
    }
    return result;
}

void CreateNewAccount()
{
    var foodBalance = ReadDecimal("Type the the money amount for the food balance: ");
    var mealBalance = ReadDecimal("Type the the money amount for the meal balance: ");
    var cashBalance = ReadDecimal("And finally, type the the money amount for the cash balance: ");
    Console.WriteLine("Please, wait while we create your account");
    var newAccount = accountService.CreateAccount(foodBalance, mealBalance, cashBalance);
    Console.WriteLine($"This is your new account: {newAccount}");
}

void MakeTransaction()
{
    var accountId = ReadInt("Please, type the id from the target account: ");
    var amount = ReadDecimal("How much do you want to pay in this transaction? ");
    var merchant = ReadString("What is the merchant you are transferring the amount to? ");
    var mcc = ReadInt("""
        For this transaction, we need the Merchant Category Code (MCC). We have three options:
        For FOOD merchants, use '5411' or '5412'.
        For MEAL merchans, use '5811' or '5812'. 
        For another types, use any integer number. 
        Now, type the MCC: 
        """);
    Console.WriteLine("Thank you! Now wait while we do the transaction");
    var newTransaction = transactionService.CreateTransaction(new Transaction(0, accountId, amount, merchant, mcc));
    if (newTransaction.IsApproved)
    {
        Console.WriteLine("The transaction was approved.");
    }
    else
    {
        Console.WriteLine($"The transaction was refused. This is the reson: {newTransaction.RejectionCause}");
    }
    var chosenAction = ReadString("Would you like to check the account balance? Type 'y' for yes and 'n' for no ");
    if (chosenAction == "y")
    {
        var account = accountService.GetAccountById(newTransaction.AccountId);
        Console.WriteLine($"This is the current balance: {account}");
    }
}

void ConsultTransactionsByAccountId()
{
    var accountId = ReadInt("Type the accountId you would like to consult the transactions: ");
    var transactions = transactionService.GetTransactionsByAccountId(accountId);
    if (transactions.Any())
    {
        Console.WriteLine($"This is the list of transactions linked to the given account id:");
        foreach (var transaction in transactions)
        {
            Console.WriteLine(transaction);
        }
    }
    else
    {
        Console.WriteLine("There is no transactions linked to the given account id");
    }
}

void ConsultTransactionsByMerchant()
{
    var merchant = ReadString("Type the merchant you would like to consult: ");
    var transactions = transactionService.GetTransactionsByMerchant(merchant ?? "default");
    if (transactions.Any())
    {
        Console.WriteLine($"This is the list of transactions linked to the given merchant:");
        foreach (var transaction in transactions)
        {
            Console.WriteLine(transaction);
        }
    }
    else
    {
        Console.WriteLine("There is no transactions linked to the given merchant");
    }
}

void ConsultAccount()
{
    var accountId = ReadInt("Type the accountId from the account you would like to consult: ");
    var account = accountService.GetAccountById(accountId);
    if (account is null)
    {
        Console.WriteLine("Account not found");
    }
    Console.WriteLine($"This is the account: {account}");
}