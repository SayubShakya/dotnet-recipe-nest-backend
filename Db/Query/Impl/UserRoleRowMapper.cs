using MySql.Data.MySqlClient;
using RecipeNest.Projection;

namespace RecipeNest.Db.Query.Impl;

public class UserRoleRowMapper : IRowMapper<UserRole>
{
    public UserRole Map(MySqlDataReader reader)
    {
        var id = reader.GetInt32("id");
        var firstName = reader.GetString("first_name");
        var lastName = reader.GetString("last_name");
        var phoneNumber = reader.GetString("phone_number");
        var email = reader.GetString("email");
        var password = reader.GetString("password");
        var role = reader.GetString("role");
        var isActive = reader.GetBoolean("is_active");

        int imageUrlOrdinal = reader.GetOrdinal("image_url");
        int aboutOrdinal = reader.GetOrdinal("about");

        string? imageUrl = reader.IsDBNull(imageUrlOrdinal)
            ? null
            : reader.GetString(imageUrlOrdinal);

        string? about = reader.IsDBNull(aboutOrdinal)
            ? null
            : reader.GetString(aboutOrdinal);

        return new UserRole(id, firstName, lastName, phoneNumber, imageUrl, about, email, password, role, isActive);
    }
}