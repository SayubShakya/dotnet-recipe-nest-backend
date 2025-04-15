// Favorite.cs

using MessagePack;

// Required for DateTime

namespace RecipeNest.Model;

public class Favorite
{
    public Favorite()
    {
    }

    public Favorite(int id, int recipeId, int userId)
    {
        Id = id;
        RecipeId = recipeId;
        UserId = userId;
    }

    public int Id { get; set; }

    public int RecipeId { get; set; }

    public int UserId { get; set; }

    public override string ToString()
    {
        return $"Favorite ID: {Id}, User ID: {UserId}, Recipe ID: {RecipeId}";
    }
}