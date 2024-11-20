using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Services.Calculation;

public abstract class BaseRebateCalculationService : IRebateCalculationService
{
    public CalculateRebateResult Calculate(CalculateRebateRequest request, Rebate rebate, Product product)
    {
        if (rebate == null)
        {
            throw new ArgumentNullException(nameof(rebate));
        }

        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        var flag = Enum.Parse<SupportedIncentiveType>(rebate.Incentive.ToString()); //TODO see class-level comment in SupportedIncentiveType.cs
        if (!product.SupportedIncentives.HasFlag(flag))
        {
            //TODO indicate to caller that product doesn't support incentive type and the calculation failed
            return new CalculateRebateResult();
        }
       
        return CalculateRebate(request, rebate, product);
    }

    protected abstract CalculateRebateResult CalculateRebate(CalculateRebateRequest request, Rebate rebate, Product product);
}