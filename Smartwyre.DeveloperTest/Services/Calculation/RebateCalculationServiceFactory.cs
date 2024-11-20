using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Calculation;

public class RebateCalculationServiceFactory : IRebateCalculationServiceFactory
{
    public IRebateCalculationService GetCalculationService(IncentiveType type)
    {
        switch (type)
        {
            case IncentiveType.FixedRateRebate:
                return new FixedRateRebateCalculationService();
            case IncentiveType.AmountPerUom:
                return new AmountPerUomRebateCalculationService();
            case IncentiveType.FixedCashAmount:
                return new FixedCashAmountRebateCalculationService();
            default:
                throw new System.Exception($"Unable to resolve rebate calculation service for type {type}");
        }
    }
}