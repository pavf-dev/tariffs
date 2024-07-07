using Tariffs.Calculation;
using Tariffs.Tariffs;
using Tariffs.Utils;

namespace Tariffs.Tests;

public class BasicTariffCostCalculatorTests
{
    [Test]
    public void Calculates_expected_cost()
    {
        var tariff = new Tariff("Packaged", "Provider", TariffType.Basic, 5, 0.21m, 0, TimePeriodInMonths.Year);
        var calculator = new BasicTariffCostCalculator();
        
        // Act
        var cost = calculator.Calculate(tariff, 3051);
        
        // Assert
        Assert.That(cost, Is.EqualTo(700.71m));
    }
    
    [Test]
    public void Throws_exception_when_tariff_type_is_not_basic()
    {
        var tariff = new Tariff("Packaged", "Provider", TariffType.Packaged, 5, 0.21m, 0,TimePeriodInMonths.Year);
        var calculator = new BasicTariffCostCalculator();
        
        // Act
        TestDelegate act = () => calculator.Calculate(tariff, 0);
        
        // Assert
        Assert.That(act, Throws.ArgumentException);
    }
}