# Bank Transactions

## Overview

The Bank Transactions is a console application developed in C# that allows users to simulate banking transactions categorized by Merchant Category Codes (MCC). MCCs are used in the banking industry to classify businesses by the type of goods or services they provide.

## Features

1. **Create Transactions**: Add new transactions.
2. **List Transactions**: Display a list of all transactions, filtered by Merchant or AccountId.
3. **Create Account**: Add new account to simulate the transactions.
4. **View Account**: Display the account balance, filtered by AccountId. 

## Why Use Record Instead of Classes for Banking Transactions?

In C# 9 and onwards, a new feature called `record` was introduced. This is especially useful for immutable data types and brings several advantages when modeling banking transactions:

1. **Value Semantics**: Records are value types. This means when you compare two records, C# will check if their content is the same, not their references.
2. **Immutability**: Once a banking transaction is created, it shouldn't be modified. Using records ensures that our transactions are immutable by default.
3. **With Expressions**: If we ever need a modified copy of a record (for some other operations), we can use `with` expressions to create a new record with some values changed.
4. **Concise Syntax**: Record types can be more concise than class definitions. This can make our code cleaner and more readable.

## Future Improvements

- **Database Integration**: The next step for this application would be integrating with a database like SQL Server or SQLite to persist transaction data. SQLite is a good option when thinking about study purpose.
  
- **Enhanced MCC Lookup**: Integrate with external APIs or databases to retrieve descriptions or more details for a given MCC, also adding more code options, since here we are just using FOOD, MEAL and CASH MCC.
  
- **Security Features**: Implement security measures like data encryption and user authentication to ensure data integrity and privacy.

## Unit Testing

To ensure the reliability and correctness of the Bank Transactions Simulator, I've implemented a comprehensive suite of unit tests. These tests validate the basic behavior of the application and act as a safety net against potential regressions.

### Test Scenarios:

1. **Transaction Approved in FOOD**
   
2. **Transaction Approved in MEAL**

3. **Transaction Refused**

4. **Transaction Approved in CASH**

5. **Two Consecutive Transactions, one approved and other refused**

By performing these tests, we can confidently say that the application correctly handles different MCCs and different transaction outcomes.

To run the tests, navigate to the project directory and use the following command:

```
dotnet test
```

## Getting Started

1. Clone the repository:
```
git clone [repository-url]
```

2. Navigate to the project directory and build the solution:
```
cd BankTransactions
dotnet build
```

3. Run the application:
```
dotnet run
```

Follow the on-screen prompts to simulate banking transactions!
