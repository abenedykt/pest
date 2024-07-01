namespace Pest.Parcel.Domain;

public class Dim
{
    public Dim(double value, MeasurementUnit unit = MeasurementUnit.Meter)
    {
        if (value <= 0)
            throw new ArgumentException("Value cannot be negative or zero");
        Value = value;
        Unit = unit;
    }
    
    public MeasurementUnit Unit { get; }
    public double Value { get; }

    public static Dim operator +(Dim first, Dim other)
    {
        if (first.Unit == other.Unit)
        {
            return new Dim(first.Value + other.Value, first.Unit);
        }

        if (Conversion.TryGetValue((first.Unit, other.Unit), out var convert))
        {
            return new Dim(convert(first.Value, other.Value), first.Unit);
        }

        throw new ArgumentException("Cannot add dimensions with given units");
    }
    
    private static readonly Dictionary<(MeasurementUnit, MeasurementUnit), Func<double, double, double>> Conversion = new()
    {
        { (MeasurementUnit.Meter, MeasurementUnit.Centimeter), (first, other) => first + other / 100 },
        { (MeasurementUnit.Centimeter, MeasurementUnit.Meter), (first, other) => first + other * 100 },
        { (MeasurementUnit.Inch, MeasurementUnit.Meter), (first, other) => first + other * 39.37007874015748 },
        { (MeasurementUnit.Meter, MeasurementUnit.Inch), (first, other) => first + other / 39.37007874015748 },
        { (MeasurementUnit.Centimeter, MeasurementUnit.Inch), (first, other) => first + other * 2.54 },
        { (MeasurementUnit.Inch, MeasurementUnit.Centimeter), (first, other) => first + other / 2.54 }
    };
}