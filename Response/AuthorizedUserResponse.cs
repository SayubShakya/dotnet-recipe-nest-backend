using MessagePack;

namespace RecipeNest.Response;

[MessagePackObject]
public class AuthorizedUserResponse
{
    public AuthorizedUserResponse(int? id, string name, string? role)
    {
        Id = id;
        Name = name;
        Role = role;
    }

    [Key("id")] public int? Id { get; set; }

    [Key("name")] public string Name { get; set; }

    [Key("role")] public string? Role { get; set; }
}