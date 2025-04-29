using MessagePack;

namespace RecipeNest.Request;

[MessagePackObject]
public class UpdateUserProfileRequest
{
    public UpdateUserProfileRequest()
    {
    }

    [Key("first_name")] public string FirstName { get; set; }

    [Key("last_name")] public string LastName { get; set; }

    [Key("phone_number")] public string PhoneNumber { get; set; }

    [Key("image_url")] public string? ImageUrl { get; set; }

    [Key("about")] public string? About { get; set; }
}