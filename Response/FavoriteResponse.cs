using MessagePack;

namespace RecipeNest.Response;

[MessagePackObject]
public class FavoriteResponse
{
    public FavoriteResponse()
    {
    }

    public FavoriteResponse(int id, int userId, int recipeId)
    {
        Id = id;
        UserId = userId;
        RecipeId = recipeId;
    }

    [Key("id")] public int Id { get; set; }

    [Key("user_id")] public int UserId { get; set; }

    [Key("recipe_id")] public int RecipeId { get; set; }
}