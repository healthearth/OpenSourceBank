// Filepath: fintechs-exhibitu/02_Application_Logic/DTOs/PhysicalAssetDeposit.cs
// © 2026 Andrew Kieckhefer. All rights reserved.

namespace GlobalBank.Application.DTOs;

public record PhysicalAssetDeposit(
    string SerialNumber, 
    decimal FaceValue, 
    string CurrencyCode, // USD, VND, CRC
    string VaultLocation // e.g., "Library-Shelf-A1"
);
