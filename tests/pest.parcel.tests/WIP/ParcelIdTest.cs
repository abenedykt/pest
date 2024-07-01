using AutoFixture.Xunit2;
using FluentAssertions;
using Pest.Parcel.Domain;

namespace Pest.Parcel.Tests.WIP;

public class ParcelIdTest
{
    [Fact]
    public void When_parcel_id_is_created_with_value_greater_than_zero_it_has_value()
    {
        var parcelId = (ParcelId)1;
        Assert.True(parcelId.HasValue);
    }

    [Fact]
    public void When_parcel_id_is_created_with_value_less_than_or_equal_to_zero_it_does_not_have_value()
    {
        Assert.Throws<ArgumentException>(() => (ParcelId)0);
    }

    [Theory, AutoData]
    public void When_parcel_id_is_created_value_is_set_correctly(long value)
    {
        ParcelId parcelId = value;

        parcelId.Value.Should().Be(value);
    }
}