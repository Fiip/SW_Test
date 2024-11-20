using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Calculation;

public interface IRebateCalculationServiceFactory
{
    IRebateCalculationService GetCalculationService(IncentiveType type);
}
