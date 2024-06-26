using pest.tracking;

namespace pest.tests.e2e;

public class PersonTests
{
    [Fact]
    public void Name_and_surname_returns_given_values()
    {
        var x = new Person("Miguel", "Camba");
        Assert.Equal("Miguel", x.Name);
        Assert.Equal("Camba", x.Surname);
    }

    [Fact]
    public void ToString_returns_name_and_surname()
    {
        var x = new Person("Miguel", "Camba");
        Assert.Equal("Miguel Camba", x.ToString());
    }
}