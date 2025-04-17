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
            RecipeDetail = reader.GetString("recipe_detail"),
            Ingredients = reader.GetString("ingredients"),
            RecipeByUserId = reader.GetInt32("recipe_by"),
            CuisineId = reader.GetInt32("cuisine"),
            Rating =  reader.GetInt32("rating"),
            IsFavorite = reader.GetBoolean("is_favorite"),
        };
    }
}