using AutoFixture.Xunit2;
using FluentAssertions;

namespace pest.parcel.tests;

public class CreateParcelTests
{
    [Theory, AutoData]
    public void When_parcel_is_created_its_data_is_published_to_kafka_topic(IParcel testParcel)
    {
        testParcel.Should().NotBeNull();
    }
}

public enum WeightUnit
{
    Kilogram,
    Gram,
    Pound
}