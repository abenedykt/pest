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
    
    [Theory,
    InlineData(10, MeasurementUnit.Meter, 10,MeasurementUnit.Meter, 20),
    InlineData(10, MeasurementUnit.Centimeter, 10, MeasurementUnit.Centimeter, 20),
    InlineData(10, MeasurementUnit.Inch, 10, MeasurementUnit.Inch, 20)
    ]
    public void When_dims_are_added_with_same_units_the_result_is_sum_of_values(double value1, MeasurementUnit unit1, double value2, MeasurementUnit unit2, double expected)
    {
        var dim1 = new Dim(value1, unit1);
        var dim2 = new Dim(value2, unit2);

        var result = dim1 + dim2;

        result.Unit.Should().Be(unit1);
        result.Value.Should().Be(expected);
    }


    [Theory,
        InlineData(10, MeasurementUnit.Meter, 10, MeasurementUnit.Centimeter, 10.10),
        InlineData(10, MeasurementUnit.Centimeter, 1, MeasurementUnit.Meter, 110),
        InlineData(10, MeasurementUnit.Inch, 1, MeasurementUnit.Meter, 49.37007874015748),
        InlineData(10, MeasurementUnit.Meter, 1, MeasurementUnit.Inch, 10.0254),
        InlineData(10, MeasurementUnit.Centimeter, 1, MeasurementUnit.Inch, 12.54),
        InlineData(10, MeasurementUnit.Inch, 1, MeasurementUnit.Centimeter, 10.393700787401574)
    ]
    public void When_dims_have_different_measurement_units_they_are_converted_and_then_added(double value1,
        MeasurementUnit unit1, double value2, MeasurementUnit unit2, double expected)
    {
        var dim1 = new Dim(value1, unit1);
        var dim2 = new Dim(value2, unit2);

        var result = dim1 + dim2;

        result.Unit.Should().Be(unit1);
        result.Value.Should().Be(expected); // <- comparing doubles without delta is not recommended
    }
}