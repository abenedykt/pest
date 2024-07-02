namespace Pest.Parcel.Endpoints;

public class CreateParcelRequest
{
    public Sender? Sender { get; set; }
    public Recipient? Recipient { get; set; }
    public ParcelClass Class { get; set; }
}

public class Sender
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string PostalCode { get; set; }
}

public class Recipient
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string PostalCode { get; set; }
}

public enum ParcelClass
{
    Standard,
    Express
}
