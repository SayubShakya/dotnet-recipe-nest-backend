
using MySql.Data.MySqlClient;
using RecipeNest.Entity;

namespace RecipeNest.Db.Query.Impl.Entity;

public class CuisineRowMapper : IRowMapper<Cuisine>
{
    public Cuisine Map(MySqlDataReader reader)
    {
        return new Cuisine
        {
            Id = reader.GetInt32("id"),
            Name = reader.GetString("name"),
            ImageUrl = reader.GetString("image_url"),
        };
    }
}