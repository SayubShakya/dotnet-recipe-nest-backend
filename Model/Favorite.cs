// Favorite.cs

using MessagePack;

// Required for DateTime

namespace RecipeNest.Model;

[MessagePackObject]
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

    [Key("id")] public int Id { get; set; }

    [Key("recipe_id")] public int RecipeId { get; set; }

    [Key("user_id")] public int UserId { get; set; }

    public override string ToString()
    {
        return $"Favorite ID: {Id}, User ID: {UserId}, Recipe ID: {RecipeId}";
    }
}