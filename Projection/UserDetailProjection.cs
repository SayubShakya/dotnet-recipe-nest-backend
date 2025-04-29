namespace RecipeNest.Projection;

public class UserDetailProjection
{
    public required int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Email { get; set; }
    public required string Role { get; set; }
    public required string About { get; set; }
    public required string ImageUrl { get; set; }
    public required bool IsActive { get; set; }
}