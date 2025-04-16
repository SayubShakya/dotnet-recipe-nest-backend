// Db/Query/Impl/RecipeRowMapper.cs

using MySql.Data.MySqlClient;
using RecipeNest.Entity;

namespace RecipeNest.Db.Query.Impl.Entity;

public class RecipeRowMapper : IRowMapper<Recipe>
{
    public Recipe Map(MySqlDataReader reader)
    {
        var id = reader.GetInt32("id");
        var imageUrl = reader.IsDBNull(reader.GetOrdinal("image_url")) ? null : reader.GetString("image_url");
        var title = reader.GetString("title");
        var description = reader.IsDBNull(reader.GetOrdinal("description"))
            ? null
            : reader.GetString("description");
        var recipeDetail = reader.GetString("recipe");
        var ingredients = reader.GetString("ingredients");
        int? recipeByUserId = reader.IsDBNull(reader.GetOrdinal("recipe_by")) ? null : reader.GetInt32("recipe_by");
        int? cuisineId = reader.IsDBNull(reader.GetOrdinal("cuisine")) ? null : reader.GetInt32("cuisine");
        return new Recipe(id, imageUrl, title, description, recipeDetail, ingredients, recipeByUserId, cuisineId);
    }
}