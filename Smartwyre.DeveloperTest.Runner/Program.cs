using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Services.Calculation;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
          .ConfigureServices(services =>
          {
              services.AddTransient<IRebateService, RebateService>();
              services.AddTransient<IProductDataStore, ProductDataStore>();
              services.AddTransient<IRebateDataStore, RebateDataStore>();
              services.AddSingleton<IRebateCalculationServiceFactory, RebateCalculationServiceFactory>();
          })
          .Build();

        var rebateService = host.Services.GetService<IRebateService>();
        var result = rebateService.Calculate(new CalculateRebateRequest { ProductIdentifier = "xxx", RebateIdentifier = "yyy", Volume = 800 });
    }
}