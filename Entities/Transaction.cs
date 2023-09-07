namespace BankingTransations.Entities;

public record Transaction(int Id, int AccountId, decimal Amount, string Merchant, int Mcc)
{
    public bool IsApproved { get; init; }
    public string? RejectionCause { get; init; }
}
