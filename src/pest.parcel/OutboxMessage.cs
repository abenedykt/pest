namespace Pest.Parcel;

public class OutboxMessage
{
    public int Id { get; set; }
    public string Data { get; set; }
    public DateTime CreatedAt { get; set; }
}