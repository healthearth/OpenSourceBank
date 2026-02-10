// Filepath: fintechs-exhibitu/01_Core_Domain/Entities/Account.cs
// © 2026 Andrew Kieckhefer. All rights reserved.

namespace GlobalBank.Domain.Entities;

public class Account
{
    public Guid Id { get; private set; }
    public string OwnerName { get; private set; }
    public string CurrencyCode { get; private set; } = "USD";
    public bool IsKycVerified { get; private set; }

    // Optional: remove Balance entirely if ledger-derived
    public decimal Balance { get; private set; }

    public Account(string owner, string currency)
    {
        Id = Guid.NewGuid();
        OwnerName = owner;
        CurrencyCode = currency;
        Balance = 0m;
    }

    public void MarkKycVerified()
    {
        IsKycVerified = true;
    }

    // Optional: controlled balance mutation
    public void ApplyDelta(decimal amount)
    {
        Balance += amount;
    }
}
