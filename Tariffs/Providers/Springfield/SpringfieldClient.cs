using System.Net.Http.Json;

namespace Tariffs.Providers.Springfield;

public interface ISpringfieldApiClient
{
    Task<IEnumerable<SpringfieldTariff>> GetTariffs(CancellationToken cancellationToken);
}

public class SpringfieldApiClientMock : ISpringfieldApiClient
{
    public Task<IEnumerable<SpringfieldTariff>> GetTariffs(CancellationToken cancellationToken)
    {
        var tariffs = new List<SpringfieldTariff>()
        {
            new ("Basic electricity tariff", SpringfieldTariffType.Basic, 5, 0.22m, 0),
            new ("Electricity tariff plus", SpringfieldTariffType.Plus, 800, 0.31m, 4000),
            new ("Electricity tariff premium", SpringfieldTariffType.Premium, 900, 0.30m, 5000)
        };
        
        return Task.FromResult(tariffs as IEnumerable<SpringfieldTariff>);
    }
}

public class SpringfieldApiClient : ISpringfieldApiClient
{
    private readonly HttpClient _httpClient;

    public SpringfieldApiClient(HttpClient httpClient)
    {
        // TODO: implement named http client with a dedicated settings
        _httpClient = httpClient;
    }
    
    public async Task<IEnumerable<SpringfieldTariff>> GetTariffs(CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync("api/tariffs", cancellationToken);
        response.EnsureSuccessStatusCode();
        
        var tariffs = await response.Content.ReadFromJsonAsync<IEnumerable<SpringfieldTariff>>(cancellationToken: cancellationToken);
        
        return tariffs;
    }
}