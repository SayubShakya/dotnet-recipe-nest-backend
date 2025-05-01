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
            ImageUrl =HandleOptionalImageUrl(reader),
            About = HandleOptionalAbout(reader),
            IsActive = reader.GetBoolean("is_active"),
        };
    }
    
    private static string? HandleOptionalImageUrl(MySqlDataReader reader)
    {
        try
        {
            return reader.GetString("image_url");
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    
    private static string? HandleOptionalAbout(MySqlDataReader reader)
    {
        try
        {
            return reader.GetString("about");
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}