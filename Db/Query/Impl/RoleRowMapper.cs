using MySql.Data.MySqlClient;
using RecipeNest.Model;

namespace RecipeNest.Db.Query.Impl
{
    public class RoleRowMapper : IRowMapper<Role>
    {
        public Role Map(MySqlDataReader reader)
        {
            int id = reader.GetInt32("id");
            string name = reader.GetString("name");
            return new Role(id, name);
        }
    }
}

