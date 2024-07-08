using FluentValidation;
using Pest.Parcel.Endpoints;

namespace Pest.Parcel.Tests;

public class SenderValidator : AbstractValidator<Sender>
{
    public SenderValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name of sender is required");
        RuleFor(x => x.Address).NotEmpty().WithMessage("Address of sender is required");
        RuleFor(x => x.PostalCode).NotEmpty().WithMessage("Postal code of sender is required");
        RuleFor(x => x.Country).NotEmpty().WithMessage("Country of sender is required");
    }
}

