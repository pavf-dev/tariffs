using FluentValidation;
using Tariffs.Calculation;
using Tariffs.Tariffs;

namespace Tariffs.Host;

public static class TariffsExtension
{
    public static void AddTariffs(this IServiceCollection services)
    {
        services.AddSingleton<ITariffService, TariffService>();
        services.AddSingleton<IValidator<Tariff>, TariffValidator>();
        services.AddSingleton<TariffCostCalculatorFactory>();
        services.AddSingleton<TariffCostsService>();
    }
}