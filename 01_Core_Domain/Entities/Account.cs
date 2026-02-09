// Filepath: fintechs-exhibitu/01_Core_Domain/Entities/Account.cs
namespace GlobalBank.Domain.Entities;

public class Account {
    public Guid Id { get; set; }
    public string OwnerName { get; private set; }
    public string CurrencyCode { get; set; } = "USD";
    public decimal Balance { get; set; }
    public bool IsKycVerified { get; private set; }

    public Account(string owner, string currency) {
        Id = Guid.NewGuid();
        OwnerName = owner;
        CurrencyCode = currency;
    }
}
