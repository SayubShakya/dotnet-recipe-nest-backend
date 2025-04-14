// UserRowMapper.cs

using MySql.Data.MySqlClient;
using RecipeNest.Model;

namespace RecipeNest.Db.Query.Impl;

public class UserRowMapper : IRowMapper<User>
{
    public User Map(MySqlDataReader reader)
    {
        var id = reader.GetInt32("id");
        var firstName = reader.GetString("first_name");
        var lastName = reader.GetString("last_name");
        var phoneNumber = reader.GetString("phone_number");
        var email = reader.GetString("email");
        var password = reader.GetString("password");
        var roleId = reader.GetInt32("role_id");

        int imageUrlOrdinal = reader.GetOrdinal("image_url");
        int aboutOrdinal = reader.GetOrdinal("about");

        string? imageUrl = reader.IsDBNull(imageUrlOrdinal)
            ? null
            : reader.GetString(imageUrlOrdinal);

        string? about = reader.IsDBNull(aboutOrdinal)
            ? null
            : reader.GetString(aboutOrdinal);

        return new User(id, firstName, lastName, phoneNumber, imageUrl, about, email, password, roleId);
    }
}