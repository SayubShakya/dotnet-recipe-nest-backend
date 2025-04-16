using MessagePack;

namespace RecipeNest.Response;

[MessagePackObject]
public class UserTableResponse
{
    public UserTableResponse()
    {
    }

    [Key("id")] public required int Id;
    [Key("first_name")] public required string FirstName;
    [Key("last_name")] public required string LastName;
    [Key("phone_number")] public required string PhoneNumber;
    [Key("email")] public required string Email;
    [Key("role")] public required string Role;
    [Key("is_active")] public required bool IsActive;
}