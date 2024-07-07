using Tariffs.Tariffs;

namespace Tariffs.Calculation;

public class BasicTariffCostCalculator : ITariffCostCalculator
{
    public decimal Calculate(Tariff tariff, int consumptionPerYear)
    {
        if (tariff.Type != TariffType.Basic)
        {
            throw new ArgumentException($"Product with basic tariff type is expected. Received tariff type {tariff.Type}");
        }
        
        var annualCost = tariff.BaseCost * tariff.TariffPeriodInMonths;
        var consumptionCost = consumptionPerYear * tariff.AdditionalCostPerUnit;

        return annualCost + consumptionCost;
    }
}