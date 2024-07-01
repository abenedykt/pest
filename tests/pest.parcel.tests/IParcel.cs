namespace pest.parcel.tests;

public interface IParcel
{
    ParcelId Id { get; }
    // Sender Sender { get; }
    // Recipient Recipient { get; }
    Weight Weight { get; }
    Dimensions Dimensions { get; }
}