using FluentAssertions;
using FluentValidation;
using Pest.Parcel.Endpoints;

namespace Pest.Parcel.Tests;

public class MyFluentValidationTests
{
    [Fact]
    public void Experimetns()
    {
        var p = new Sender();
        var validator = new SenderValidator();
        
        var result = validator.Validate(p);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(4);
    }
        
}

public class RecipientValidator : AbstractValidator<Recipient>
{
    RecipientValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name of recipient is required");
        RuleFor(x => x.Address).NotEmpty().WithMessage("Address of recipient is required");
        RuleFor(x => x.PostalCode).NotEmpty().WithMessage("Postal code of recipient is required");
        RuleFor(x => x.Country).NotEmpty().WithMessage("Country of recipient is required");
    }
}

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

