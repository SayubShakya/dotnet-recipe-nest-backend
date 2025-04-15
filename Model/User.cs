// User.cs

using MessagePack;

namespace RecipeNest.Model;

public class User
{
    public User()
    {
    }

    public User(int id, string firstName, string lastName, string phoneNumber, string? imageUrl, string? about,
        string email, string password, int roleId, bool isActive)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        ImageUrl = imageUrl;
        About = about;
        Email = email;
        Password = password;
        RoleId = roleId;
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

    public int RoleId { get; set; }
    public bool IsActive { get; set; }

    public override string ToString()
    {
        return
            $"User ID: {Id}, Name: {FirstName} {LastName}, Email: {Email}, Phone: {PhoneNumber}, RoleId: {RoleId}";
    }
}