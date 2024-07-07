using Tariffs.Tariffs;

namespace Tariffs.Calculation;

public class TariffCostCalculatorFactory
{
    public ITariffCostCalculator Create(TariffType type)
    {
        return type switch
        {
            TariffType.Basic => new BasicTariffCostCalculator(),
            TariffType.Packaged => new PackagedTariffCostCalculator(),
            _ => throw new ArgumentException($"Unsupported tariff type {type}")
        };
    }
}