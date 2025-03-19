using MySql.Data.MySqlClient;
using Model;

namespace Mapper.Implementation
{

    public class RoleRowMapper : RowMapper<Role>
    {
        public Role MapRow(MySqlDataReader reader)
        {
            int id = reader.GetInt32("id");
            string name = reader.GetString("name");
            return new Role(id, name);
        }
    }
}