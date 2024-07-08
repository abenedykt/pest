namespace Pest.Parcel.Endpoints;

public interface IOutbox
{
    void Publish<T>(T message);
}