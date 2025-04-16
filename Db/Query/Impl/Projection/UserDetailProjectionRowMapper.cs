using MySql.Data.MySqlClient;
using RecipeNest.Projection;

namespace RecipeNest.Db.Query.Impl.Projection;

public class UserDetailProjectionRowMapper : IRowMapper<UserDetailProjection>
{
    public UserDetailProjection Map(MySqlDataReader reader)
    {
        return new UserDetailProjection()
        {
            Id = reader.GetInt32("id"),
            FirstName = reader.GetString("first_name"),
            LastName = reader.GetString("last_name"),
            PhoneNumber = reader.GetString("phone_number"),
            Email = reader.GetString("email"),
            Role = reader.GetString("role"),
            IsActive = reader.GetBoolean("is_active"),
            About = reader.GetString("about"),
            ImageUrl = reader.GetString("image_url"),
        };
    }
}