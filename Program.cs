// fintechs-exhibitu/Program.cs

var tenDollars = new PhysicalAssetDeposit(
    faceValue: 10m,
    currencyCode: "USD",
    serialNumber: "PK25146493B"
);

var service = new CapitalRegistrationService(repo);

await service.RegisterStartupCapitalAsync(
    targetAccountId: foundersCapitalAccountId,
    assets: new[] { tenDollars }
);

var fiftyDong = new PhysicalAssetDeposit(
    faceValue: 50m, 
    currencyCode: "VND",
    serialNumber: "IS16631076"
);

await service.RegisterStartupCapitalAsync(
    targetAccountId: foundersCapitalAccountId,
    assets: new[] { fiftyDong }
);


var twentyOSB = new PhysicalAssetDeposit(
    faceValue: 1m, 
    currencyCode: "OSB",
    serialNumber: "395150299"
);

await service.RegisterStartupCapitalAsync(
    targetAccountId: foundersCapitalAccountId,
    assets: new[] { oneOSB }
);

var costaRicaGoldCoin = new PhysicalAssetDeposit( 
    faceValue: 500m, // 500 CRC 
    currencyCode: "CRC", 
    serialNumber: "CRC-COIN-500-20260212-001R" // generated unique ID
); 

await service.RegisterStartupCapitalAsync( 
    targetAccountId: foundersCapitalAccountId, 
    assets: new[] { costaRicaGoldCoin } 
);

var btcDeposit = new PhysicalAssetDeposit(
    faceValue: 0.00010521m,              // 0.00010521 BTC
    currencyCode: "BTC",
    serialNumber: "BTC-WALLET-KEY-20260212-001" // internal unique ID
);

await service.RegisterStartupCapitalAsync(
    targetAccountId: foundersCapitalAccountId,
    assets: new[] { btcDeposit }
);

