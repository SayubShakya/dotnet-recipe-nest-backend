using MessagePack;

namespace RecipeNest.Response;

[MessagePackObject]
public class RoleResponse
{
    public RoleResponse()
    {
    }

    public RoleResponse(int id, string name)
    {
        Id = id;
        Name = name;
    }

    [Key("id")] public int Id { get; set; }

    [Key("name")] public string Name { get; set; }
}