namespace Pest.Parcel.Endpoints;

public interface IPuidClient
{
    Task<string> Fetch();
}