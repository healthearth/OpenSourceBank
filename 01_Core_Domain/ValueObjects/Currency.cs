// Filepath: fintechs-exhibitu/01_Core_Domain/ValueObjects/Currency.cs
// © 2026 Andrew Kieckhefer. All rights reserved.

namespace GlobalBank.Domain.ValueObjects;

public record Currency {
    public string Code { get; } // e.g., "AI$"
    public string Symbol { get; } // e.g., "🤖"
    
    public static Currency AiDollar => new Currency("AI$", "🤖");
    public static Currency Usd => new Currency("USD", "$");

    private Currency(string code, string symbol) {
        Code = code;
        Symbol = symbol;
    }
}
