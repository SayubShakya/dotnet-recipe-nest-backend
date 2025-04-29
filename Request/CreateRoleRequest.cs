using MessagePack;

namespace RecipeNest.Request;

[MessagePackObject]
public class CreateRoleRequest
{
    public CreateRoleRequest()
    {
    }

    public CreateRoleRequest(string name)
    {
        Name = name;
    }

    [Key("name")] public string Name { get; set; }

    public override string ToString()
    {
        return $"Role, Name: {Name}";
    }
}