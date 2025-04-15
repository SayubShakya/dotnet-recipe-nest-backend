// Db/Query/Impl/RecipeRowMapper.cs

using MySql.Data.MySqlClient;
using RecipeNest.Model;
using RecipeNest.Projection;

namespace RecipeNest.Db.Query.Impl;

public class RecipeAuthorizedRowMapper : IRowMapper<RecipeAuthorized>
{
    public RecipeAuthorized Map(MySqlDataReader reader)
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
        int? rating = reader.IsDBNull(reader.GetOrdinal("rating")) ? null : reader.GetInt32("rating");
        bool? isFavorite = reader.IsDBNull(reader.GetOrdinal("is_favorite")) ? null : reader.GetBoolean("is_favorite");
        return new RecipeAuthorized(id, imageUrl, title, description, recipeDetail, ingredients, recipeByUserId, cuisineId, isFavorite, rating);
    }
}