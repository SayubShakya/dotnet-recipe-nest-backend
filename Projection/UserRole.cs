// User.cs

using RecipeNest.Model;

namespace RecipeNest.Projection;

public class UserRole
{
    public UserRole()
    {
    }

    public UserRole(int id, string firstName, string lastName, string phoneNumber, string? imageUrl, string? about,
        string email, string password, string role, bool isActive)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        ImageUrl = imageUrl;
        About = about;
        Email = email;
        Password = password;
        Role = role;
        IsActive = isActive;
    }

    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PhoneNumber { get; set; }

    public string? ImageUrl { get; set; }

    public string? About { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string Role { get; set; }
    public bool IsActive { get; set; }
}