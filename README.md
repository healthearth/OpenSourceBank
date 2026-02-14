# OpenSourceBank (formerly fintechs-exhibitu)
My crack at a payment processing community for creators.

![AndrewsHarryPotterMondayForecast](https://github.com/user-attachments/assets/e0135414-85d8-4bb5-9e75-9e9c59ed0753)

# Progress

<img width="1196" height="281" alt="Screenshot 2026-02-09 223218" src="https://github.com/user-attachments/assets/1dfaa3e3-47a7-40ac-acaf-1c64d2a6d2d3" />

> Double-entry ledger first financial platform is born with audit logs built in




OpenLedger-Atlas/
├── 01_Core_Domain/             # THE HEART: Rules of the Bank
│   ├── Entities/               # Account.cs, Transaction.cs, LedgerEntry.cs
|   |   ├── User.cs
│   │   ├── Account.cs
│   │   ├── LedgerEntry.cs
│   │   └── Transaction.cs
|   ├── Enums/
│   ├── ValueObjects/           # Money.cs, Currency.cs (Handles international math)
│   └── Interfaces/             # IBankingRepository.cs (The "Blueprint")
│
├── 02_Application_Logic/       # THE BRAIN: Use Cases
│   ├── Transfers/              # P2PTransferService.cs (Venmo-style logic)
│   ├── Merchant/
│   │   └── MerchantSettlementService.cs
│   ├── Services/
│   │   ├── AuthService.cs
│   │   ├── TransferService.cs
│   │   └── TokenService.cs
│   ├── Compliance/             # AMLMonitor.cs (Flags >$10k transactions)
│   └── DTOs/                   # Data Transfer Objects (Secure data packets)
│
├── 03_Infrastructure/          # THE ARMS: External Vendors
│   ├── Data/
│   │   ├── OpenLedgerDbContext.cs
│   │   └── Migrations/
│   ├── Persistence/            # SQL_Ledger_Context.cs (Talks to the Database)
│   ├── PaymentGateway/         # Stripe_Adapter.cs (Independent Merchant Service)
│   └── Vault_Sync/             # Physical_Audit_Logger.cs (Connects to the Library)
│
├── 04_Presentation_API/        # THE FACE: Entry Point
│   ├── Controllers/            # PaymentsController.cs, UserSettingsController.cs
│   │   ├── AuthController.cs
│   │   ├── AccountController.cs
│   │   └── TransferController.cs
│   └── Middleware/             # SecurityHeaders.cs (Prevents hacking)
│
├── 05_Internal_Audit_Portal/   # THE LAW: Admin View
|   └── Reports/                # Generating legal PDF reports for regulators
|
├── 06_Eventing/        
│   ├── IDomainEvent.cs            
│   ├── TransactionCreatedEvent.cs
│   ├── FraudDetectedEvent.cs
│   ├── CurrencyFluctuatedEvent.cs
│   └── SimpleEventBus.cs
│               
├── 07_Simulation_Engine/        
│   ├── TransactionSimulator.cs            
│   ├── CurrencyVolatilitySimulator.cs
│   └── FraudScenarioGenerator.cs
│               
├── 08_Forecasting/        
│   └── CashflowForecastService.cs  
|
├── 09_Observability/        
│   ├── frontend-dashboard/            
│   │   ├── index.html
│   │   ├── styles.css
│   │   └── app.js
│   ├── docker/            
│   │   ├── Docker
│   │   └── docker-compose.yml
│   ├── .github/            
│   │   └── workflows
│   ├── docs/            
│   │   ├── 
│   │   
│   └── MetricsService.cs/            
|  
├── README.md            
├── Docker
└── docker-compose.yml



# OpenLedger Platform

OpenLedger is a real-time value exchange platform with authenticated users, persistent ledger storage, and transaction auditability.

## Features

- JWT Authentication
- Real PostgreSQL persistence
- Double-entry transfer logic
- Transaction logging
- Observability metrics
- Dockerized deployment

## Architecture

ASP.NET Core 8  
PostgreSQL  
Docker  
JWT Security  

## Local Setup

docker-compose up --build

API: http://localhost:5000

## Demo Flow

1. Register two users
2. Login both
3. Transfer funds
4. Verify balances
5. Query metrics endpoint

## Production Deployment

Deploy via Render using Docker.
Set environment variables for DB and JWT.


