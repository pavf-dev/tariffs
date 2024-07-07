using Tariffs.Providers;
using Tariffs.Tariffs;

namespace Tariffs.Host.Providers;

public class TariffUpdaterTimeTrigger<T> where T : IProvider
{
    private readonly ITariffUpdater<T> _tariffUpdater;
    private readonly string _providerName;
    private readonly ILogger<TariffUpdaterTimeTrigger<T>> _logger;
    private PeriodicTimer _timer;
    private CancellationTokenSource _cts = new();
    private Task _work;

    public TariffUpdaterTimeTrigger(
        ITariffUpdater<T> tariffUpdater,
        ILogger<TariffUpdaterTimeTrigger<T>> logger)
    {
        _tariffUpdater = tariffUpdater;
        _providerName = typeof(T).Name;
        _logger = logger;
        // TODO: get interval from config
        _timer = new PeriodicTimer(TimeSpan.FromMinutes(1));
        _work = Task.CompletedTask;
    }

    public void Start()
    {
        _work = DoWork();
    }
    
    private async Task DoWork()
    {
        try
        {
            
            await _tariffUpdater.UpdateTariffs(_cts.Token);
            
            while (await _timer.WaitForNextTickAsync(_cts.Token))
            {
                try
                {
                    await _tariffUpdater.UpdateTariffs(_cts.Token);
                }
                catch (Exception e) when(e is not OperationCanceledException)
                {
                    _logger.LogError(e, "Exception from tariff update of {Provider}", _providerName);
                }
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Tariff updater for {Provider} is stopped by request.", _providerName);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unhandled exception from tariff updater of {Provider}. Tariff updater is stopped.", _providerName);
        }
    }

    public async Task StopAsync()
    {
        await _cts.CancelAsync();
        await _work;
        _cts.Dispose();
    }
}