namespace Tariffs.Providers.Springfield;

public record SpringfieldTariff(
    string Name,
    SpringfieldTariffType Type,
    decimal BaseCost,
    decimal AdditionalCostPerUnit,
    int UnitsIncludedInBaseCost);
    
public enum SpringfieldTariffType
{
    Unknown,
    Basic,
    Plus,
    Premium
}