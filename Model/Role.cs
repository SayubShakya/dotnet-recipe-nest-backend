// Role.cs

using MessagePack;

namespace RecipeNest.Model;

public class Role
{
    public Role()
    {
    }

    public Role(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public override string ToString()
    {
        return $"Role ID: {Id}, Name: {Name}";
    }
}