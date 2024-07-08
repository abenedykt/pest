using FluentValidation;
using Pest.Parcel.Endpoints;

namespace Pest.Parcel.Tests;

public class RecipientValidator : AbstractValidator<Recipient>
{
    public RecipientValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name of recipient is required");
        RuleFor(x => x.Address).NotEmpty().WithMessage("Address of recipient is required");
        RuleFor(x => x.PostalCode).NotEmpty().WithMessage("Postal code of recipient is required");
        RuleFor(x => x.Country).NotEmpty().WithMessage("Country of recipient is required");
    }
}