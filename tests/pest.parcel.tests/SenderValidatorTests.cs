using FluentAssertions;
using FluentValidation.TestHelper;
using Pest.Parcel.Endpoints;

namespace Pest.Parcel.Tests;

public class SenderValidatorTests
{
    [Fact]
    public void Should_have_all_validations()
    {
        var p = new Sender();
        var validator = new SenderValidator();
        
        var testValidate = validator.TestValidate(p);

        testValidate.IsValid.Should().BeFalse();
        testValidate.ShouldHaveValidationErrorFor(x => x.Name);
        testValidate.ShouldHaveValidationErrorFor(x => x.Address);
        testValidate.ShouldHaveValidationErrorFor(x => x.PostalCode);
        testValidate.ShouldHaveValidationErrorFor(x => x.Country);
        
    }
        
}