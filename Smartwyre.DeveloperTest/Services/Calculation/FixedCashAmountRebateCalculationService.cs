using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Calculation;

public class FixedCashAmountRebateCalculationService : BaseRebateCalculationService
{
    protected override CalculateRebateResult CalculateRebate(CalculateRebateRequest request, Rebate rebate, Product product)
    {
        if (rebate.Amount == 0 )
        {
            //TODO indicate to caller that zero amount was found and the calculation failed
            return new CalculateRebateResult();
        }

        return new CalculateRebateResult { Amount = rebate.Amount, Success = true };
    }
}