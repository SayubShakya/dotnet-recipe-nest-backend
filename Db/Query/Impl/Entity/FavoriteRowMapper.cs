// FavoriteRowMapper.cs

using MySql.Data.MySqlClient;
using RecipeNest.Entity;

namespace RecipeNest.Db.Query.Impl.Entity;

public class FavoriteRowMapper : IRowMapper<Favorite>
{
    public Favorite Map(MySqlDataReader reader)
    {
        return new Favorite
        {
            Id = reader.GetInt32("id"),
            RecipeId = reader.GetInt32("recipe_id"),
            UserId = reader.GetInt32("user_id"),
        };
    }
}