using Tariffs.Tariffs;

namespace Tariffs.Calculation;

public class TariffCostsService
{
    private readonly ITariffService _tariffService;
    private readonly TariffCostCalculatorFactory _costCalculatorFactory;

    public TariffCostsService(
        ITariffService tariffService,
        TariffCostCalculatorFactory costCalculatorFactory)
    {
        _tariffService = tariffService;
        _costCalculatorFactory = costCalculatorFactory;
    }
    
    public IEnumerable<TariffCost> CalculateCosts(int consumptionPerYear)
    {
        var tariffs = _tariffService.GetTariffs().ToList();
        var tariffCosts = new List<TariffCost>(tariffs.Count);
        
        foreach (var tariff in tariffs)
        {
            var costCalculator = _costCalculatorFactory.Create(tariff.Type);
            var annualCost = costCalculator.Calculate(tariff, consumptionPerYear);
            tariffCosts.Add(new TariffCost(tariff.Name, annualCost));
        }

        return tariffCosts;
    }
}