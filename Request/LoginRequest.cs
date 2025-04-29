using MessagePack;

namespace RecipeNest.Request;

[MessagePackObject]
public class LoginRequest
{
    public LoginRequest()
    {
    }

    [Key("email")] public string Email { get; set; }
    [Key("password")] public string Password { get; set; }
}