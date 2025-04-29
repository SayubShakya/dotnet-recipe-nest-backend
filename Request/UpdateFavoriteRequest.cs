using MessagePack;

namespace RecipeNest.Request;

[MessagePackObject]
public class UpdateFavoriteRequest
{
    public UpdateFavoriteRequest()
    {
    }

    public UpdateFavoriteRequest(int id, int userId, int recipeId)
    {
        Id = id;
        UserId = userId;
        RecipeId = recipeId;
    }

    [Key("id")] public int Id { get; set; }

    [Key("user_id")] public int UserId { get; set; }

    [Key("recipe_id")] public int RecipeId { get; set; }

    public override string ToString()
    {
        return $"UpdateFavoriteRequest: ID: {Id}, User ID: {UserId}, Recipe ID: {RecipeId}";
    }
}