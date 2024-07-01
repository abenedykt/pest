namespace pest.parcel.tests;

public class Dim
{
    public Dim(long value, MeasurementUnit unit = MeasurementUnit.Meter)
    {
        if (value <= 0)
            throw new ArgumentException("Value cannot be negative or zero");
        Value = value;
        Unit = unit;
    }
    
    public MeasurementUnit Unit { get; }
    public double Value { get; }
}