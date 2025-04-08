// CreateFavoriteRequest.cs

using MessagePack;

namespace RecipeNest.Request;

[MessagePackObject]
public class CreateFavoriteRequest
{
    public CreateFavoriteRequest()
    {
    }

    public CreateFavoriteRequest(int userId, int recipeId)
    {
        UserId = userId;
        RecipeId = recipeId;
    }

    [Key("user_id")] public int UserId { get; set; }

    [Key("recipe_id")] public int RecipeId { get; set; }

    public override string ToString()
    {
        return $"CreateFavoriteRequest: User ID: {UserId}, Recipe ID: {RecipeId}";
    }
}