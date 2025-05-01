using MySql.Data.MySqlClient;
using RecipeNest.Projection;

namespace RecipeNest.Db.Query.Impl.Projection;

public class FavoriteRecipeProjectionRowMapper : IRowMapper<RecipeProjection>
{
    public RecipeProjection Map(MySqlDataReader reader)
    {
        return new RecipeProjection()
        {
            Id = reader.GetInt32("id"),
            ImageUrl = reader.GetString("image_url"),
            Title = reader.GetString("title"),
            Description = reader.GetString("description"),
            RecipeDetail = reader.GetString("recipe"),
            Ingredients = reader.GetString("ingredients"),
            RecipeByUserId = reader.GetInt32("recipe_by"),
            CuisineId = reader.GetInt32("cuisine"),
            Rating = HandleOptional(reader),
            IsFavorite = null
        };
    }

    private static int? HandleOptional(MySqlDataReader reader)
    {
        try
        {
            return reader.GetInt32("rating");
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}