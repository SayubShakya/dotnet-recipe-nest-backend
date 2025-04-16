using MessagePack;
using RecipeNest.Entity;

namespace RecipeNest.Request;

[MessagePackObject]
public class CreateRatingRequest
{
    public CreateRatingRequest()
    {
    }

    public CreateRatingRequest(int userId, int recipeId, RatingScore score)
    {
        UserId = userId;
        RecipeId = recipeId;
        Score = score;
    }

    [Key("user_id")] public int UserId { get; set; }

    [Key("recipe_id")] public int RecipeId { get; set; }

    [Key("rating")] public RatingScore Score { get; set; }

    public override string ToString()
    {
        return $"CreateRatingRequest: UserID: {UserId}, RecipeID: {RecipeId}, Score: {Score}";
    }
}