using MySql.Data.MySqlClient;
using RecipeNest.Projection;
using RecipeNest.Response;

namespace RecipeNest.Db.Query.Impl.Projection;

public class ChefTableProjectionRowMapper : IRowMapper<ChefTableProjection>
{
    public ChefTableProjection Map(MySqlDataReader reader)
    {
        return new ChefTableProjection()
        {
            FirstName = reader.GetString("first_name"),
            LastName = reader.GetString("last_name"),
            PhoneNumber = reader.GetString("phone_number"),
            Email = reader.GetString("email"),
        };
    }
}