using pest.examples;


namespace pest.performance.tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var x = new Person("Miguel", "Camba");
        Assert.Equal("Miguel", x.Name);
        Assert.Equal("Camba", x.Surname);
    }
}