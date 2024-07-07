using Tariffs.Providers.Springfield;
using Tariffs.Tariffs;

namespace Tariffs.Host.Providers.Springfield;

public static class SpringfieldExtension
{
    public static void AddSpringfield(this IServiceCollection services)
    {
        services.AddHostedService<SpringfieldHostService>();
        services.AddSingleton<TariffUpdaterTimeTrigger<SpringfieldProvider>>();
        services
            .AddSingleton<ITariffUpdater<SpringfieldProvider>, TariffUpdater<SpringfieldProvider>>();
        services.AddSingleton<SpringfieldProvider>();
        services.AddSingleton<ISpringfieldApiClient, SpringfieldApiClientMock>();
    }
}