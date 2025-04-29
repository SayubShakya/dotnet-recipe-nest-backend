using MySql.Data.MySqlClient;
using RecipeNest.Projection;

namespace RecipeNest.Db.Query.Impl.Projection;

public class RecipeProjectionRowMapper : IRowMapper<RecipeProjection>
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
            Rating = !reader.IsDBNull(11) ? reader.GetInt32("rating") : 0,
            IsFavorite = !reader.IsDBNull(12) && reader.GetBoolean("is_favorite")
        };
    }
}