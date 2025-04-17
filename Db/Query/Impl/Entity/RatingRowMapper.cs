using MySql.Data.MySqlClient;
using RecipeNest.Entity;

namespace RecipeNest.Db.Query.Impl.Entity;

public class RatingRowMapper : IRowMapper<Rating>
{
    public Rating Map(MySqlDataReader reader)
    {
        RatingScore? score = null;
        var ratingString = reader.GetString("rating");
        if (Enum.TryParse(ratingString, out RatingScore parsedScore))
        {
            score = parsedScore;
        }

        return new Rating
        {
            Id = reader.GetInt32("id"),
            RecipeId = reader.GetInt32("recipe_id"),
            UserId = reader.GetInt32("user_id"),
            Score = score
        };
    }
}