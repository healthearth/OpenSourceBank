// Filepath: fintechs-exhibitu/01_Core_Domain/ValueObjects/Money.cs
// © 2026 Andrew Kieckhefer. All rights reserved.

public record Money {
    public decimal Amount { get; init; }
    public string Currency { get; init; } // e.g. "AI$"

    public Money(decimal amount, string currency) {
        if (amount < 0) throw new ArgumentException("Money cannot be negative");
        Amount = Math.Round(amount, 2); // Regulatory standard precision
        Currency = currency;
    }
}
