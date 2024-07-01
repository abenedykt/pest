namespace pest.parcel.tests;

public class ParcelId
{
    private ParcelId(long value)
    {
        if (value <= 0)
            throw new ArgumentException("Parcel id must be greater than 0");
        Value = value;
    }
    public long Value { get; }
    
    public bool HasValue=> Value > 0;

    public static implicit operator ParcelId(long value) => new(value);
}