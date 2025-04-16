using MessagePack;
using RecipeNest.Entity;

namespace RecipeNest.Response;

[MessagePackObject]
public class RatingResponse
{
    public RatingResponse()
    {
    }

    public RatingResponse(int id, int userId, int recipeId, RatingScore score)
    {
        Id = id;
        UserId = userId;
        RecipeId = recipeId;
        Score = score;
    }

    [Key("id")] public int Id { get; set; }

    [Key("user_id")] public int UserId { get; set; }

    [Key("recipe_id")] public int RecipeId { get; set; }

    [Key("rating")] public RatingScore Score { get; set; }
}