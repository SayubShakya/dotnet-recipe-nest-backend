using MessagePack;

namespace RecipeNest.Request;

[MessagePackObject]
public class UpdateRoleRequest
{
    public UpdateRoleRequest()
    {
    }

    public UpdateRoleRequest(int id, string name)
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