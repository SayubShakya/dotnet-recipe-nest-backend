// FavoriteResponse.cs
using MessagePack;

namespace RecipeNest.Reponse
{
    [MessagePackObject]
    public class FavoriteResponse
    {
        [Key("id")]
        public int Id { get; set; }

        [Key("user_id")]
        public int UserId { get; set; }

        [Key("recipe_id")]
        public int RecipeId { get; set; }

        public FavoriteResponse() { }

        public FavoriteResponse(int id, int userId, int recipeId)
        {
            Id = id;
            UserId = userId;
            RecipeId = recipeId;
        }

        public override string ToString()
        {
            return $"FavoriteResponse: ID: {Id}, User ID: {UserId}, Recipe ID: {RecipeId}";
        }
    }
}