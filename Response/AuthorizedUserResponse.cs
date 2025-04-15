using MessagePack;

namespace RecipeNest.Response;

[MessagePackObject]
public class AuthorizedUserResponse
{
    public AuthorizedUserResponse(string name, string? role)
    {
        Name = name;
        Role = role;
    }

    [Key("name")] public string Name { get; set; }

    [Key("role")] public string? Role { get; set; }
}