// UserRowMapper.cs

using MySql.Data.MySqlClient;
using RecipeNest.Model;

namespace RecipeNest.Db.Query.Impl;

public class UserRowMapper : IRowMapper<User>
{
    public User Map(MySqlDataReader reader)
    {
        var id = reader.GetInt32("id");
        var FirstName = reader.GetString("first_name");
        var LastName = reader.GetString("last_name");
        var PhoneNumber = reader.GetString("phone_number");
        var ImageUrl = reader.GetString("image_url");
        var About = reader.GetString("about");
        var Email = reader.GetString("email");
        var Password = reader.GetString("password");
        var RoleId = reader.GetInt32("role_id");
        return new User(id, FirstName, LastName, PhoneNumber, ImageUrl, About, Email, Password, RoleId);
    }
}