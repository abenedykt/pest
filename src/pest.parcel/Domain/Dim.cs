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



        throw new NotImplementedException();
    }
    
}