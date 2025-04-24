namespace RecipeNest.Entity;

public class User
{
    public int Id { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string PhoneNumber { get; set; }

    public string? ImageUrl { get; set; }

    public string? About { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public int RoleId { get; set; }
    public bool IsActive { get; set; }
}