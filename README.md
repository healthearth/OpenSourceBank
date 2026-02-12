# OpenSourceBank (formerly fintechs-exhibitu)
My crack at a payment processing community for creators.

![AndrewsHarryPotterMondayForecast](https://github.com/user-attachments/assets/e0135414-85d8-4bb5-9e75-9e9c59ed0753)

# Roadmap

## PHASE 1 — “WE CAN MOVE VALUE” (0–3 months)

> Goal: A working wallet with internal transfers

## PHASE 2 — “OUR OWN CURRENCY” (3–6 months)

> Goal: Exist in the digital currency world

## PHASE 3 — SOCIAL + MONEY = GRAVY (6–12 months)

> Goal: Exhibitu has a framework

## PHASE 4 — “MERCHANT SERVICES” (12–24 months)

> Goal: Independence



# Progress

<img width="1196" height="281" alt="Screenshot 2026-02-09 223218" src="https://github.com/user-attachments/assets/1dfaa3e3-47a7-40ac-acaf-1c64d2a6d2d3" />

> Double-entry ledger first financial platform is born with audit logs built in





fintechs-exhibitu/
├── 01_Core_Domain/             # THE HEART: Rules of the Bank
│   ├── Entities/               # Account.cs, Transaction.cs, LedgerEntry.cs
│   ├── ValueObjects/           # Money.cs, Currency.cs (Handles international math)
│   └── Interfaces/             # IBankingRepository.cs (The "Blueprint")
│
├── 02_Application_Logic/       # THE BRAIN: Use Cases
│   ├── Transfers/              # P2PTransferService.cs (Venmo-style logic)
│   ├── Compliance/             # AMLMonitor.cs (Flags >$10k transactions)
│   └── DTOs/                   # Data Transfer Objects (Secure data packets)
│
├── 03_Infrastructure/          # THE ARMS: External Vendors
│   ├── Persistence/            # SQL_Ledger_Context.cs (Talks to the Database)
│   ├── PaymentGateway/         # Stripe_Adapter.cs (Independent Merchant Service)
│   └── Vault_Sync/             # Physical_Audit_Logger.cs (Connects to the Library)
│
├── 04_Presentation_API/        # THE FACE: Entry Point
│   ├── Controllers/            # PaymentsController.cs, UserSettingsController.cs
│   └── Middleware/             # SecurityHeaders.cs (Prevents hacking)
│
└── 05_Internal_Audit_Portal/   # THE LAW: Admin View
    └── Reports/                # Generating legal PDF reports for regulators


# OpenSourceBank — Monetary Constitution & System Overview  
© 2026 Andrew Kieckhefer. All rights reserved.

## 1. Purpose

OpenSourceBank (“OSB”) is a ledger-based monetary system designed to support multi-currency accounting, asset-backed internal money, and transparent auditability. OSB Coin (“OSB”) is the native internal unit of account used for settlement, rewards, and treasury operations inside the OpenSourceBank ledger. OSB ID is US Social Security numbering.

This document defines the legal, technical, and monetary rules governing the OSB system.

---

## 2. Currency framework

### 2.1 Supported currencies

OSB supports the following founding currency classes:

| Type       | Code | Symbol |
|------------|------|--------|
| Internal   | OSB  | °      |
| Fiat       | USD  | $      |
| Fiat       | VND  | ₫      |
| Fiat       | CRC  | ₡      |
| Crypto     | BTC  | ₿      |
| Crypto     | ETH  | Ξ      |

All currencies are represented as immutable Value Objects (`Currency`) and paired with `Money` value objects for amount + currency code.

---

## 3. Treasury and issuance

### 3.1 Treasury account

The OSB Treasury is the sole authority permitted to mint OSB. It is represented as a protected account within the ledger and:

- Cannot be deleted  
- Cannot be renamed  
- Cannot be converted to another currency  

The Treasury holds the initial OSB supply and any subsequent system-level issuances approved by governance.

### 3.2 Genesis mint

The OSB Genesis Mint is a one-time issuance event that creates the initial supply of OSB and credits it to the Treasury account.

Properties:

- Executed exactly once  
- Recorded as a dedicated “Genesis Mint” transaction  
- Immutable and fully auditable  
- Cannot be reversed or repeated  

### 3.3 Minting restrictions

- No entity other than the Treasury may mint OSB.  
- No implicit, hidden, or off-ledger minting is permitted.  
- All issuance must be represented as explicit ledger transactions.  
- Any change to total OSB supply must be traceable to a governance-approved event. (In this case "event" is defined as measured characteristics of current weather variables and location).

---

## 4. OSB monetary policy

### 4.1 Initial supply

The initial OSB supply is defined at Genesis and credited entirely to the Treasury. The exact numeric value is set in code (e.g., `1_000_000_000 OSB` id 393692129) and documented in release notes.

### 4.2 Supply model

By default, OSB follows a **fixed-supply** model:

- No automatic inflation  
- No algorithmic expansion  
- No seigniorage outside explicit governance decisions  

Any deviation from fixed supply (e.g., controlled inflation, reward pools) must be:

- Explicitly documented  
- Versioned  
- Approved via governance  
- Implemented as auditable ledger events

### 4.3 Backing and reserves

OSB may be economically backed by:

- Fiat deposits (e.g., USD, VND, CRC)  
- Cryptocurrency deposits (e.g., BTC, ETH)  
- Physical assets (e.g., coins, notes, metals, bullion, agriculture)
- Digital assets (intellectual property, tokenized systems, sensitve account data like passwords, significant streaming events, software)

Each backing asset is recorded as a `PhysicalAssetDeposit` and:

- Registered in the vault  
- Linked to a ledger entry  
- Included in internal audit reports  

OSB does **not** guarantee convertibility to any external asset at a fixed rate. Backing is economic and accounting-based, not a legal redemption promise. Those involved assume all risk.

### 4.4 Account rules

- No account may hold a negative balance in any currency.
- A value is always assigned, regardless of how small, value must be more than 0, the absence of any value. This README.md is a OSB digital asset worth value more than 0. 
- All balance changes must be the result of valid ledger transactions.  
- OSB transfers must be OSB → OSB between valid accounts.  
- Fees, burns, or rewards (if implemented) must be explicit and auditable.

---

## 5. Ledger rules

### 5.1 Double-entry accounting

All movements of value are recorded as double-entry ledger transactions. For every credit, there is a corresponding debit.

### 5.2 Immutability

Ledger entries are permanent:

- No deletion  
- No in-place modification  
- Corrections must be made via compensating transactions  

### 5.3 Audit trail

The system maintains a complete audit trail including:

- Timestamps  
- Account IDs  
- Amounts and currencies  
- Descriptions  
- Optional physical asset references  

Audit reports may be generated for, not to become excessive nor intrusive to business activity, internal review, compliance, or external demonstration of solvency.

---

## 6. Startup capital and asset deposits

Startup capital may be contributed in any supported currency. Each contribution is:

1. Represented as a `PhysicalAssetDeposit` (e.g., USD bill, VND note, CRC coin, BTC/ETH wallet key).  
2. Registered in the vault with a unique identifier (serial number, timestamped, or internal ID).  
3. Converted to the internal unit (e.g., OSB or AI$) via `ExchangeEngine`.  
4. Credited to a designated capital or treasury account via ledger entry.  

This process establishes real economic backing for the system’s internal balances.

---

## 7. Governance

### 7.1 Authority

The system operator (currently: **Andrew Kieckhefer**) is the initial governing authority responsible for:

- Monetary policy  
- Treasury operations  
- Exchange rules  
- System upgrades 
- Access control and operational security
- Maintenance as needed or anticipated

### 7.2 Policy changes

Any change to:

- OSB supply  
- Monetary policy  
- Treasury rules  
- Backing asset treatment  

must be:

- Documented in versioned release notes  
- Reflected in code and configuration  
- Auditable via ledger events  

Future governance models (e.g., multi-sig, council, DAO-like structures) may be introduced and documented in subsequent versions.

---

## 8. Legal positioning

OSB is:

- An internal unit of account value
- Not a public cryptocurrency  
- Not a security  
- Not a commodity  
- Not marketed or sold as an investment without a clear defendable property backing (most often times a home). 

OSB is functionally analogous to:

- PayPal internal balances  
- Roblox Robux  
- Airline miles  
- Starbucks rewards  

OSB balances exist solely within the OpenSourceBank ledger and are not guaranteed to be redeemable for any external asset.
Governing bodies have the right to refuse service to anyone for any nondiscriminatory reasoning only where discriminatory will be deemed by third party if the need should occur.

---

## 9. Versioning

Current ledger and branch designations:

- Legacy ledger: `v1-ledger-legacy`  
- OSB development branch: `feature/osb-coin`  

All future changes to the monetary system, ledger schema, or governance model must be versioned and documented.

---

## 10. Contributions

Community contributions are welcome under the terms described in `contributions/CONTRIBUTING.md`. All contributors are expected to respect the monetary rules and legal positioning defined in this document.

