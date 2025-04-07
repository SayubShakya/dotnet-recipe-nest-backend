// UserRowMapper.cs

using MySql.Data.MySqlClient;
using RecipeNest.Model;
using RecipeNest.Db.Query;

namespace RecipeNest.Db.Query.Impl
{
    public class UserRowMapper : IRowMapper<User>
    {
        public User Map(MySqlDataReader reader)
        {
            int id = reader.GetInt32("id");
            string FirstName = reader.GetString("first_name");
            string LastName = reader.GetString("last_name");
            string PhoneNumber = reader.GetString("phone_number");
            string ImageUrl = reader.GetString("image_url");
            string About = reader.GetString("about");
            string Email = reader.GetString("email");
            string Password = reader.GetString("password");
            int RoleId = reader.GetInt32("role_id");
            return new User(id, FirstName, LastName, PhoneNumber, ImageUrl, About, Email, Password, RoleId);
        }
    }
}