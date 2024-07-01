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
        
        if(first.Unit == MeasurementUnit.Meter && other.Unit == MeasurementUnit.Centimeter)
        {
            return new Dim(first.Value + other.Value / 100, MeasurementUnit.Meter);
        }

        if (first.Unit == MeasurementUnit.Centimeter && other.Unit == MeasurementUnit.Meter)
        {
            return new Dim(first.Value + other.Value * 100, MeasurementUnit.Centimeter);
        }
        
        if(first.Unit == MeasurementUnit.Inch && other.Unit == MeasurementUnit.Meter)
        {
            return new Dim(first.Value + other.Value * 39.37007874015748, MeasurementUnit.Inch);
        }
        
        if(first.Unit == MeasurementUnit.Meter && other.Unit == MeasurementUnit.Inch)
        {
            return new Dim(first.Value + other.Value / 39.37007874015748, MeasurementUnit.Meter);
        }
        
        if(first.Unit == MeasurementUnit.Centimeter && other.Unit == MeasurementUnit.Inch)
        {
            return new Dim(first.Value + other.Value * 2.54, MeasurementUnit.Centimeter);
        }
        
        if(first.Unit == MeasurementUnit.Inch && other.Unit == MeasurementUnit.Centimeter)
        {
            return new Dim(first.Value + other.Value / 2.54, MeasurementUnit.Inch);
        }
        
        throw new ArgumentException("Cannot add dimensions with given units");
    }
}