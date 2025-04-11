// Request/RegisterRequest.cs

using MessagePack;

namespace RecipeNest.Request;

[MessagePackObject]
public class RegisterRequest
{
    public RegisterRequest()
    {
    }

    [Key("first_name")] public string FirstName { get; set; }

    [Key("last_name")] public string LastName { get; set; }

    [Key("phone_number")] public string PhoneNumber { get; set; }

    [Key("email")] public string Email { get; set; }

    [Key("password")] public string Password { get; set; }
    
    [Key("role_id")] public int RoleId { get; set; }

}