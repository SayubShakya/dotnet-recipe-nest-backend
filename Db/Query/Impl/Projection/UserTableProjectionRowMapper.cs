using MySql.Data.MySqlClient;
using RecipeNest.Projection;

namespace RecipeNest.Db.Query.Impl.Projection;

public class UserTableProjectionRowMapper : IRowMapper<UserTableProjection>
{
    public UserTableProjection Map(MySqlDataReader reader)
    {
        return new UserTableProjection()
        {
            Id = reader.GetInt32("id"),
            FirstName = reader.GetString("first_name"),
            LastName = reader.GetString("last_name"),
            PhoneNumber = reader.GetString("phone_number"),
            Email = reader.GetString("email"),
            Role = reader.GetString("role"),
            IsActive = reader.GetBoolean("is_active"),
        };
    }
}