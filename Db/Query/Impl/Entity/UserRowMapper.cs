using MySql.Data.MySqlClient;
using RecipeNest.Entity;

namespace RecipeNest.Db.Query.Impl.Entity;

public class UserRowMapper : IRowMapper<User>
{
    public User Map(MySqlDataReader reader)
    {
        return new User
        {
            Id = reader.GetInt32("id"),
            FirstName = reader.GetString("first_name"),
            LastName = reader.GetString("last_name"),
            PhoneNumber = reader.GetString("phone_number"),
            Email = reader.GetString("email"),
            Password = reader.GetString("password"),
            RoleId = reader.GetInt32("role_id"),
            ImageUrl = reader.GetString("image_url"),
            About = reader.GetString("about"),
            IsActive = reader.GetBoolean("is_active"),
        };
    }
}