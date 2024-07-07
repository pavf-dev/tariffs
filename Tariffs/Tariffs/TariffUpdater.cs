using Microsoft.Extensions.Logging;
using Tariffs.Providers;

namespace Tariffs.Tariffs;

public interface ITariffUpdater<T> where T : IProvider
{
    Task UpdateTariffs(CancellationToken cancellationToken);
}

public class TariffUpdater<T> : ITariffUpdater<T> where T : IProvider
{
    private readonly T _tariffProvider;
    private readonly ITariffService _tariffService;
    private readonly ILogger<TariffUpdater<T>> _logger;

    public TariffUpdater(
        T tariffProvider,
        ITariffService tariffService,
        ILogger<TariffUpdater<T>> logger)
    {
        _tariffProvider = tariffProvider;
        _tariffService = tariffService;
        _logger = logger;
    }

    public async Task UpdateTariffs(CancellationToken cancellationToken)
    {
        var tariffs = await _tariffProvider.GetTariffs(cancellationToken);
        var updateResult = _tariffService.AddOrUpdateTariffs(tariffs.ToList(), _tariffProvider.Name);

        if (updateResult.ValidationFailed)
        {
            _logger.LogError("Tariff update failed for {Provider}. Errors: {Errors}",
                _tariffProvider.Name, updateResult.ValidationErrors);
        }
    }
}