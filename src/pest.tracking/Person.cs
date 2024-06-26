namespace pest.tracking;

public class Person(string name, string surname)
{
    public string Name { get; } = name;
    public string Surname { get; } = surname;

    public override string ToString() => $"{Name} {Surname}".Trim();
}