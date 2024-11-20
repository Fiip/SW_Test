using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Calculation;

public interface IRebateCalculationService
{
    CalculateRebateResult Calculate(CalculateRebateRequest request, Rebate rebate, Product product);
}
