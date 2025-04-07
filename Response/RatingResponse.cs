using MessagePack;
using RecipeNest.Model;

namespace RecipeNest.Reponse
{
    [MessagePackObject]
    public class RatingResponse
    {
        [Key("id")] public int Id { get; set; }

        [Key("user_id")] public int UserId { get; set; }

        [Key("recipe_id")] public int RecipeId { get; set; }

        [Key("rating")] public RatingScore Score { get; set; }

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

        public override string ToString()
        {
            return $"RatingResponse: ID: {Id}, UserID: {UserId}, RecipeID: {RecipeId}, Score: {Score}";
        }
    }
}