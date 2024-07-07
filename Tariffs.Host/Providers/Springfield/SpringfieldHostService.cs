using Tariffs.Providers.Springfield;

namespace Tariffs.Host.Providers.Springfield;

public class SpringfieldHostService : IHostedService
{
    private readonly TariffUpdaterTimeTrigger<SpringfieldProvider> _tariffUpdaterTimeTrigger;

    public SpringfieldHostService(
        TariffUpdaterTimeTrigger<SpringfieldProvider> tariffUpdaterTimeTrigger)
    {
        _tariffUpdaterTimeTrigger = tariffUpdaterTimeTrigger;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _tariffUpdaterTimeTrigger.Start();
        
        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _tariffUpdaterTimeTrigger.StopAsync();
    }
}