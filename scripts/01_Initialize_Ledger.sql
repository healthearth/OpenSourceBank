-- Filepath: scripts/01_Initialize_Ledger.sql
-- © 2026 Andrew Kieckhefer. All rights reserved.

-- THE ASSET VAULT: Tracks every physical bill by serial number
CREATE TABLE PhysicalVault (
    SerialNumber NVARCHAR(50) PRIMARY KEY,
    CurrencyCode NVARCHAR(3) NOT NULL, -- USD, VND, CRC
    FaceValue DECIMAL(18, 4) NOT NULL,
    LocationCode NVARCHAR(50) DEFAULT 'Main_Library_Vault',
    Status NVARCHAR(20) DEFAULT 'In_Vault', -- In_Vault, Transit, Out
    CreatedAt DATETIME2 DEFAULT GETUTCDATE()
);

-- THE DIGITAL LEDGER: Every move of AI$ must be recorded here
CREATE TABLE DigitalLedger (
    EntryId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    AccountId UNIQUEIDENTIFIER NOT NULL,
    Debit DECIMAL(18, 4) DEFAULT 0,
    Credit DECIMAL(18, 4) DEFAULT 0,
    PhysicalAssetRef NVARCHAR(50), -- Links to PhysicalVault.SerialNumber
    Description NVARCHAR(255),
    Timestamp DATETIME2 DEFAULT GETUTCDATE()
);
