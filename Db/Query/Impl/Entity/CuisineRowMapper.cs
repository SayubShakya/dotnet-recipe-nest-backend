
using MySql.Data.MySqlClient;
using RecipeNest.Entity;

namespace RecipeNest.Db.Query.Impl.Entity;

public class CuisineRowMapper : IRowMapper<Cuisine>
{
    public Cuisine Map(MySqlDataReader reader)
    {
        var id = reader.GetInt32("id");
        var Name = reader.GetString("name");
        var ImageUrl = reader.GetString("image_url");
        return new Cuisine(id, Name, ImageUrl);
    }
}