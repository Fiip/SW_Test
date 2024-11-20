using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Services.Calculation;

public class AmountPerUomRebateCalculationService : BaseRebateCalculationService
{
    protected override CalculateRebateResult CalculateRebate(CalculateRebateRequest request, Rebate rebate, Product product)
    {
        if (rebate.Amount == 0 || request.Volume == 0)
        {
            //TODO indicate to caller that zero amount/volume was found and the calculation failed
            return new CalculateRebateResult();
        }

        var amount = rebate.Amount * request.Volume;
        return new CalculateRebateResult { Amount = amount, Success = true };
    }
}