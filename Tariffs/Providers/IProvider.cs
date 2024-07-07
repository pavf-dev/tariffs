using Tariffs.Tariffs;

namespace Tariffs.Providers;

public interface IProvider
{
    string Name { get; }
    Task<IEnumerable<Tariff>> GetTariffs(CancellationToken cancellationToken);
}