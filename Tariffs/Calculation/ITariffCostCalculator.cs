using Tariffs.Tariffs;

namespace Tariffs.Calculation;

public interface ITariffCostCalculator
{
    decimal Calculate(Tariff tariff, int consumptionPerYear);
}