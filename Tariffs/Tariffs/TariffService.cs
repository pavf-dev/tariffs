using System.Collections.Concurrent;
using System.Text;
using FluentValidation;

namespace Tariffs.Tariffs;

public interface ITariffService
{
    IEnumerable<Tariff> GetTariffs();
    (bool ValidationFailed, string ValidationErrors)  AddOrUpdateTariffs(IReadOnlyCollection<Tariff> tariffs, string providerName);
}

public class TariffService : ITariffService
{
    private readonly IValidator<Tariff> _tariffValidator;
    private readonly ConcurrentDictionary<string, IReadOnlyCollection<Tariff>> _tariffs = new();

    public TariffService(IValidator<Tariff> tariffValidator)
    {
        _tariffValidator = tariffValidator;
    }
    
    public IEnumerable<Tariff> GetTariffs()
    {
        return _tariffs.SelectMany(x => x.Value);
    }

    // TODO: Result pattern is more suitable in this case. Was done like that for time economy.
    public (bool ValidationFailed, string ValidationErrors) AddOrUpdateTariffs(IReadOnlyCollection<Tariff> tariffs, string providerName)
    {
        var validTariffs = new List<Tariff>();
        var validationErrors = new StringBuilder();
        
        foreach (var tariff in tariffs)
        {
            var validationResult = _tariffValidator.Validate(tariff);

            if (validationResult.IsValid)
            {
                validTariffs.Add(tariff);
                
                continue;
            }

            validationErrors.AppendLine($"{tariff.Name} validation errors:");
            
            foreach (var error in validationResult.Errors)
            {
                validationErrors.AppendLine(error.ErrorMessage);
            }
        }
        
        _tariffs[providerName] = validTariffs;

        return (validationErrors.Length != 0, validationErrors.Length == 0 ? string.Empty : validationErrors.ToString());
    }
}