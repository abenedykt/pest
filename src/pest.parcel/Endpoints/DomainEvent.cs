namespace Pest.Parcel.Endpoints;

public abstract class DomainEvent
{
    public abstract string EventType { get; }
    public DateTime CreatedAt { get; set; }
}