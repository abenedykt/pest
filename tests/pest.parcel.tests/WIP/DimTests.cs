using FluentAssertions;
using Pest.Parcel.Domain;

namespace Pest.Parcel.Tests.WIP;

public class DimTests
{
    [Theory, 
     InlineData(0, MeasurementUnit.Meter), 
     InlineData(0, MeasurementUnit.Centimeter), 
     InlineData(0, MeasurementUnit.Inch),
     InlineData(-1, MeasurementUnit.Meter),
     InlineData(-1, MeasurementUnit.Centimeter),
     InlineData(-1, MeasurementUnit.Inch)
    ]
    public void Dim_cannot_be_negative(long value, MeasurementUnit unit)
    {
        Assert.Throws<ArgumentException>(() => new Dim(value, unit));
    }
    
    [Fact]
    public void When_dim_is_created_the_default_unit_is_meter()
    {
        Dim dim = new Dim(10);

        dim.Unit.Should().Be(MeasurementUnit.Meter);
        dim.Value.Should().Be(10);
    }
    
    // [Fact]
    // public void When_dim_of_different_units_are_added_result_is_in_meter()
    // {
    //     var dim1 = new Dim(1);
    //     var dim2 = new Dim(100, MeasurementUnit.Centimeter);
    //
    //     Dim result = dim1 + dim2;
    //
    //     result.Unit.Should().Be(MeasurementUnit.Meter);
    //     result.Value.Should().Be(2);
    // }
    
    
}