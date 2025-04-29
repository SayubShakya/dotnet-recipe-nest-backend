using MySql.Data.MySqlClient;
using RecipeNest.Entity;

namespace RecipeNest.Db.Query.Impl.Entity;

public class RecipeRowMapper : IRowMapper<Recipe>
{
    public Recipe Map(MySqlDataReader reader)
    {
        return new Recipe()
        {
            Id = reader.GetInt32("id"),
            ImageUrl = reader.GetString("image_url"),
            Title = reader.GetString("title"),
            Description = reader.GetString("description"),
            RecipeDetail = reader.GetString("recipe"),
            Ingredients = reader.GetString("ingredients"),
            RecipeByUserId = reader.GetInt32("recipe_by"),
            CuisineId = reader.GetInt32("cuisine"),
        };
    }
}