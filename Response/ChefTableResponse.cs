using MessagePack;

namespace RecipeNest.Response;

[MessagePackObject]
public class ChefTableResponse
{
    public ChefTableResponse()
    {
    }
    
    [Key("first_name")] public required string FirstName;
    [Key("last_name")] public required string LastName;
    [Key("phone_number")] public required string PhoneNumber;
    [Key("email")] public required string Email;
}