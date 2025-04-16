using MySql.Data.MySqlClient;
using RecipeNest.Entity;

namespace RecipeNest.Db.Query.Impl.Entity;

public class RatingRowMapper : IRowMapper<Rating>
{
    public Rating Map(MySqlDataReader reader)
    {
        var id = reader.GetInt32("id");
        int? recipeId = reader.GetInt32("recipe_id");
        int? userId = reader.GetInt32("user_id");

        RatingScore? score = null;
        var ratingString = reader.GetString("rating");
        if (Enum.TryParse(ratingString, out RatingScore parsedScore)) score = parsedScore;

        return new Rating(id, score, recipeId, userId);
    }
}