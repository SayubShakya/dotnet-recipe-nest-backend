// Role.cs

using MessagePack;

namespace RecipeNest.Model;

[MessagePackObject]
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

    [Key("id")] public int Id { get; set; }

    [Key("name")] public string Name { get; set; }

    public override string ToString()
    {
        return $"Role ID: {Id}, Name: {Name}";
    }
}