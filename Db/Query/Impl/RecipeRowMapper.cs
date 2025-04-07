// Db/Query/Impl/RecipeRowMapper.cs
using MySql.Data.MySqlClient;
using RecipeNest.Model;
using RecipeNest.Db.Query;

namespace RecipeNest.Db.Query.Impl
{
    public class RecipeRowMapper : IRowMapper<Recipe>
    {
        public Recipe Map(MySqlDataReader reader)
        {
            int id = reader.GetInt32("id");
            string? imageUrl = reader.IsDBNull(reader.GetOrdinal("image_url")) ? null : reader.GetString("image_url");
            string title = reader.GetString("title");
            string? description = reader.IsDBNull(reader.GetOrdinal("description")) ? null : reader.GetString("description");
            string recipeDetail = reader.GetString("recipe");
            string ingredients = reader.GetString("ingredients");
            int? recipeByUserId = reader.IsDBNull(reader.GetOrdinal("recipe_by")) ? null : reader.GetInt32("recipe_by");
            int? cuisineId = reader.IsDBNull(reader.GetOrdinal("cuisine")) ? null : reader.GetInt32("cuisine");
            return new Recipe(id, imageUrl, title, description, recipeDetail, ingredients, recipeByUserId, cuisineId);
        }
    }
}