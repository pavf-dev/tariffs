namespace Tariffs.Tariffs;

public record Tariff(
    string Name,
    string ProviderName,
    TariffType Type,
    decimal BaseCost,
    decimal AdditionalCostPerUnit,
    int UnitsIncludedInBaseCost,
    int TariffPeriodInMonths);
    
public enum TariffType
{
    Unknown,
    Basic,
    Packaged
}