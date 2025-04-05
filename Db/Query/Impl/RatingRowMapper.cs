using MySql.Data.MySqlClient;
using RecipeNest.Db.Query;
using RecipeNest.Model;
using System; 

namespace RecipeNest.Db.Query.Impl
{
    public class RatingRowMapper : IRowMapper<Rating>
    {
        public Rating Map(MySqlDataReader reader)
        {
            int id = reader.GetInt32("id");
            int? recipeId = reader.GetInt32("recipe_id"); 
            int? userId = reader.GetInt32("user_id");     

            RatingScore? score = null;
            string ratingString = reader.GetString("rating");
            if (Enum.TryParse<RatingScore>(ratingString, out RatingScore parsedScore))
            {
                score = parsedScore;
            }

            return new Rating(id, score, recipeId, userId);
        }
    }
}