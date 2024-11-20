using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services.Calculation;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    private readonly IProductDataStore _productDataStore;
    private readonly IRebateDataStore _rebateDataStore;
    private readonly IRebateCalculationServiceFactory _calculationServiceFactory;

    public RebateService(IProductDataStore productDataStore, IRebateDataStore rebateDataStore, IRebateCalculationServiceFactory calculationServiceFactory)
    {
        _productDataStore = productDataStore;
        _rebateDataStore = rebateDataStore;
        _calculationServiceFactory = calculationServiceFactory;
    }

    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        //load data from datastore
        var rebate = _rebateDataStore.GetRebate(request.RebateIdentifier);
        var product = _productDataStore.GetProduct(request.ProductIdentifier);

        if (rebate == null)
        {
            //TODO indicate to caller that rebate wasn't found
            return new CalculateRebateResult();
        }

        if (product == null)
        {
            //TODO indicate to caller that product wasn't found
            return new CalculateRebateResult();
        }

        //resolve calculation service for incentive type
        var service = _calculationServiceFactory.GetCalculationService(rebate.Incentive);
        //run calculation
        var result = service.Calculate(request, rebate, product);

        //if successful, store
        if (result.Success)
        {
            _rebateDataStore.StoreCalculationResult(rebate, result.Amount);
        }

        return result;
    }
}