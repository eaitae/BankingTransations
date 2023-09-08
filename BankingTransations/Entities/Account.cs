namespace BankingTransations.Entities;

public record Account(
    int AccountId,
    decimal FoodBalance,
    decimal MealBalance,
    decimal CashBalance);