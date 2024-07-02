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
        { (MeasurementUnit.Meter, MeasurementUnit.Centimeter), (first, second) => first + second / MeterToCm },
        { (MeasurementUnit.Centimeter, MeasurementUnit.Meter), (first, second) => first + second * MeterToCm },
        { (MeasurementUnit.Inch, MeasurementUnit.Meter), (first, second) => first + second * InchToMeter },
        { (MeasurementUnit.Meter, MeasurementUnit.Inch), (first, second) => first + second / InchToMeter },
        { (MeasurementUnit.Centimeter, MeasurementUnit.Inch), (first, second) => first + second * InchToCm },
        { (MeasurementUnit.Inch, MeasurementUnit.Centimeter), (first, second) => first + second / InchToCm }
    };
    
    private const double MeterToCm = 100;
    private const double InchToMeter = 39.37007874015748;
    private const double InchToCm = 2.54;
}