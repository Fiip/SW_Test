using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Calculation;

public class FixedRateRebateCalculationService : BaseRebateCalculationService
{
    protected override CalculateRebateResult CalculateRebate(CalculateRebateRequest request, Rebate rebate, Product product)
    {
        if (rebate.Percentage == 0 || product.Price == 0 || request.Volume == 0)
        {
            //TODO indicate to caller that zero rebate percentage/product price/volume was found and the calculation failed
            return new CalculateRebateResult();
        }
        
        var amount = product.Price * rebate.Percentage * request.Volume;
        return new CalculateRebateResult { Amount = amount, Success = true };
    }
}