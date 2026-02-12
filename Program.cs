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
