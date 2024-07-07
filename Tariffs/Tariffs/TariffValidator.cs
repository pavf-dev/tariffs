using FluentValidation;

namespace Tariffs.Tariffs;

public class TariffValidator : AbstractValidator<Tariff>
{
    public TariffValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.ProviderName).NotEmpty();
        RuleFor(x => x.Type).IsInEnum().NotEmpty();
        RuleFor(x => x.BaseCost).GreaterThan(0);
        
        When(x => x.Type == TariffType.Basic, () =>
        {
            RuleFor(x => x.AdditionalCostPerUnit).GreaterThan(0);
        });

        When(x => x.Type == TariffType.Packaged, () =>
        {
            RuleFor(x => x.AdditionalCostPerUnit).GreaterThan(0);
            RuleFor(x => x.UnitsIncludedInBaseCost).GreaterThan(0);
        });
    }
}