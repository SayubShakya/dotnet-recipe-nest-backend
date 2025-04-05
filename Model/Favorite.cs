// Favorite.cs
using MessagePack;
using System; // Required for DateTime

namespace RecipeNest.Model
{
    [MessagePackObject]
    public class Favorite
    {
        [Key("id")]
        public int Id { get; set; }

        [Key("recipe_id")]
        public int RecipeId { get; set; } 

        [Key("user_id")]
        public int UserId { get; set; } 

        public Favorite() { }
        public Favorite(int id, int recipeId, int userId)
        {
            Id = id;
            RecipeId = recipeId;
            UserId = userId;
        }

        public override string ToString()
        {
            return $"Favorite ID: {Id}, User ID: {UserId}, Recipe ID: {RecipeId}";
        }
    }
}