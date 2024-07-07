using Tariffs.Tariffs;
using Tariffs.Utils;

namespace Tariffs.Providers.Springfield;

public class SpringfieldProvider : IProvider
{
    private readonly ISpringfieldApiClient _apiClient;

    public SpringfieldProvider(
        ISpringfieldApiClient apiClient)
    {
        _apiClient = apiClient;
        Name = "Springfield Powerplant";
    }
    
    public string Name { get; }
    
    public async Task<IEnumerable<Tariff>> GetTariffs(CancellationToken cancellationToken)
    {
        var tariffs = await _apiClient.GetTariffs(cancellationToken);
        
        return tariffs.Select(MapToDomainTariff);
    }

    private Tariff MapToDomainTariff(SpringfieldTariff tariff)
    {
        var tariffType = tariff.Type switch
        {
            SpringfieldTariffType.Basic => TariffType.Basic,
            SpringfieldTariffType.Plus => TariffType.Packaged,
            SpringfieldTariffType.Premium => TariffType.Packaged,
            _ => TariffType.Unknown
        };

        return new Tariff(tariff.Name, Name, tariffType, tariff.BaseCost, tariff.AdditionalCostPerUnit,
            tariff.UnitsIncludedInBaseCost, TimePeriodInMonths.Year);
    }
}