// Filepath: fintechs-exhibitu/01_Core_Domain/ValueObjects/Currency.cs
// © 2026 Andrew Kieckhefer. All rights reserved.

namespace GlobalBank.Domain.ValueObjects;

public record Currency {
    public string Code { get; } // e.g., "AI$"
    public string Symbol { get; } // e.g., "🤖"
    
    public static Currency AiDollar => new Currency("AI$", "🤖");

    // Fiat
    public static Currency Usdollar => new Currency("USD", "$");
    public static Currency VietnameseDong => new Currency("VND", "₫");
    public static Currency CostaRicanColon => new Currency("CRC", "₡");

    // Crypto
    public static Currency Bitcoin => new Currency("BTC", "₿"); 
    public static Currency Ethereum => new Currency("ETH", "Ξ");

    // Internal Coin 
     public static Currency Osb => new Currency("OSB", "°");
    


    private Currency(string code, string symbol) {
        Code = code;
        Symbol = symbol;
    }
}
