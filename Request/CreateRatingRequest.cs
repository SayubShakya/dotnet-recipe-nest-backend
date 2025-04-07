using MessagePack;
using RecipeNest.Model;

namespace RecipeNest.Request
{
    [MessagePackObject]
    public class CreateRatingRequest
    {
        [Key("user_id")] public int UserId { get; set; }

        [Key("recipe_id")] public int RecipeId { get; set; }

        [Key("rating")] public RatingScore Score { get; set; }

        public CreateRatingRequest()
        {
        }

        public CreateRatingRequest(int userId, int recipeId, RatingScore score)
        {
            UserId = userId;
            RecipeId = recipeId;
            Score = score;
        }

        public override string ToString()
        {
            return $"CreateRatingRequest: UserID: {UserId}, RecipeID: {RecipeId}, Score: {Score}";
        }
    }
}