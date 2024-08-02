namespace Pest.Parcel.Endpoints;

public class ParcelCreatedEvent : DomainEvent
{
    public string ID { get; set; }

    public override string EventType => "ParcelCreated";
    public Sender Sender { get; set; }
    public Recipient Recipient { get; set; }
    public ParcelClass Class { get; set; }
}