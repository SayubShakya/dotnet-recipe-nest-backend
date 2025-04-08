// RoleRowMapper.cs

using MySql.Data.MySqlClient;
using RecipeNest.Model;

namespace RecipeNest.Db.Query.Impl;

public class RoleRowMapper : IRowMapper<Role>
{
    public Role Map(MySqlDataReader reader)
    {
        var id = reader.GetInt32("id");
        var name = reader.GetString("name");
        return new Role(id, name);
    }
}