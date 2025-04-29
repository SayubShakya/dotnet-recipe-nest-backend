using MySql.Data.MySqlClient;
using RecipeNest.Entity;

namespace RecipeNest.Db.Query.Impl.Entity;

public class RoleRowMapper : IRowMapper<Role>
{
    public Role Map(MySqlDataReader reader)
    {
        return new Role
        {
            Id = reader.GetInt32("id"),
            Name = reader.GetString("name")
        };
    }
}