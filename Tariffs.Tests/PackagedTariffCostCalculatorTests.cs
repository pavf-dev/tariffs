using Tariffs.Calculation;
using Tariffs.Tariffs;
using Tariffs.Utils;

namespace Tariffs.Tests;

public class PackagedTariffCostCalculatorTests
{
    [Test]
    [TestCase(3500, 800)]
    [TestCase(4000, 800)]
    [TestCase(4001, 800.31)]
    [TestCase(5100, 1141)]
    public void Calculates_expected_cost(int units, decimal expectedCost)
    {
        var tariff = new Tariff("Packaged Tariff", "Provider", TariffType.Packaged, 800, 0.31m, 4000, TimePeriodInMonths.Year);
        var calculator = new PackagedTariffCostCalculator();
        
        // Act
        var cost = calculator.Calculate(tariff, units);
        
        // Assert
        Assert.That(cost, Is.EqualTo(expectedCost));
    }
}