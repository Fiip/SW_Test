using Moq;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Services.Calculation;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests;

public class PaymentServiceTests
{
    private readonly Mock<IProductDataStore> _productDataMock = new Mock<IProductDataStore>();
    private readonly Mock<IRebateDataStore> _rebateDataMock = new Mock<IRebateDataStore>();

    private const string ProductName = "xxx";
    private const string RebateName = "yyy";

    [Theory]
    [InlineData(IncentiveType.FixedCashAmount)]
    [InlineData(IncentiveType.FixedRateRebate)]
    [InlineData(IncentiveType.AmountPerUom)]
    public void Theory_VerifyZeroAmounts(IncentiveType incentiveType)
    {
        _productDataMock
            .Setup(p => p.GetProduct(It.Is<string>(x => x == ProductName)))
            .Returns(new Product {
                SupportedIncentives = SupportedIncentiveType.AmountPerUom| SupportedIncentiveType.FixedCashAmount | SupportedIncentiveType.FixedRateRebate
            });

        _rebateDataMock
            .Setup(r => r.GetRebate(It.Is<string>(x => x == RebateName)))
            .Returns(new Rebate
            {
                Incentive = incentiveType,
            });

        var service = new RebateService(_productDataMock.Object, _rebateDataMock.Object, new RebateCalculationServiceFactory());
        var result = service.Calculate(new CalculateRebateRequest
        {
            ProductIdentifier = ProductName,
            RebateIdentifier = RebateName,
        });

        //assert result not stored
        _rebateDataMock
             .Verify(r => r.StoreCalculationResult(
                 It.IsAny<Rebate>(),
                 It.IsAny<decimal>()), Times.Never);

        //assert success flag false
        Assert.False(result.Success);
    }

    [Theory]
    [InlineData(IncentiveType.FixedCashAmount,10)]
    public void Theory_VerifyFixedAmount(IncentiveType incentiveType, decimal amount)
    {
        _productDataMock
            .Setup(p => p.GetProduct(It.Is<string>(x => x == ProductName)))
            .Returns(new Product
            {
                SupportedIncentives =  SupportedIncentiveType.FixedCashAmount
            });

        _rebateDataMock
            .Setup(r => r.GetRebate(It.Is<string>(x => x == RebateName)))
            .Returns(new Rebate
            {
                Incentive = incentiveType,
                Amount = amount,
            });

        var service = new RebateService(_productDataMock.Object, _rebateDataMock.Object, new RebateCalculationServiceFactory());
        var result = service.Calculate(new CalculateRebateRequest
        {
            ProductIdentifier = ProductName,
            RebateIdentifier = RebateName,
        });

        //assert result stored
        _rebateDataMock
             .Verify(r => r.StoreCalculationResult(
                 It.IsAny<Rebate>(),
                 It.IsAny<decimal>()), Times.Once);

        //assert success flag true
        Assert.True(result.Success);

        //assert result numeric value
        Assert.Equal(amount, result.Amount);
    }
}