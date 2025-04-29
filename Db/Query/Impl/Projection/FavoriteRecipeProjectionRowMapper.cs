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
            Rating = GetFaultyValue(reader),
            IsFavorite = null
        };
    }

    private static int? GetFaultyValue(MySqlDataReader reader)
    {
        try
        {
            return !reader.IsDBNull(11) ? reader.GetInt32("rating") : null;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}