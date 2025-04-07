// FavoriteRowMapper.cs

using MySql.Data.MySqlClient;
using RecipeNest.Db.Query;
using RecipeNest.Model;

namespace RecipeNest.Db.Query.Impl
{
    public class FavoriteRowMapper : IRowMapper<Favorite>
    {
        public Favorite Map(MySqlDataReader reader)
        {
            int id = reader.GetInt32("id");
            int recipeId = reader.GetInt32("recipe_id");
            int userId = reader.GetInt32("user_id");
            return new Favorite(id, recipeId, userId);
        }
    }
}