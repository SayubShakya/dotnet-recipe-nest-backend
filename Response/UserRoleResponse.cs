using MessagePack;

namespace RecipeNest.Response;

[MessagePackObject]
public class UserRoleResponse
{
    public UserRoleResponse()
    {
    }

    public UserRoleResponse(int id, string firstName, string lastName, string phoneNumber, string? imageUrl,
        string? about, string email, string role, bool isActive)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        ImageUrl = imageUrl;
        About = about;
        Email = email;
        Role = role;
        IsActive = isActive;
    }

    [Key("id")] public int Id { get; set; }

    [Key("first_name")] public string FirstName { get; set; }

    [Key("last_name")] public string LastName { get; set; }

    [Key("phone_number")] public string PhoneNumber { get; set; }

    [Key("image_url")] public string? ImageUrl { get; set; }

    [Key("about")] public string? About { get; set; }

    [Key("email")] public string Email { get; set; }

    [Key("role")] public string Role { get; set; }

    [Key("is_active")] public bool IsActive { get; set; }

    public override string ToString()
    {
        return $"UserRoleResponse ID: {Id}, Name: {FirstName} {LastName}, Role: {Role}, Email: {Email}";
    }
}