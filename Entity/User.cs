namespace RecipeNest.Entity;

public class User
{
    public int Id { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string PhoneNumber { get; set; }

    public string? ImageUrl { get; set; }

    public string? About { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }

    public required int RoleId { get; set; }
    public bool IsActive { get; set; }
}