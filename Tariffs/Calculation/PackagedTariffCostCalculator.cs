using Tariffs.Tariffs;

namespace Tariffs.Calculation;

public class PackagedTariffCostCalculator : ITariffCostCalculator
{
    public decimal Calculate(Tariff tariff, int consumptionPerYear)
    {
        if (tariff.Type != TariffType.Packaged)
        {
            throw new ArgumentException($"Product with packaged tariff type is expected. Received tariff type {tariff.Type}");
        }
        
        var annualCost = tariff.BaseCost;
        var consumptionCost = consumptionPerYear > tariff.UnitsIncludedInBaseCost
            ? tariff.AdditionalCostPerUnit * (consumptionPerYear - tariff.UnitsIncludedInBaseCost)
            : 0;

        return annualCost + consumptionCost;
    }
}