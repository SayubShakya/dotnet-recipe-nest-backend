using MySql.Data.MySqlClient;

namespace Mapper
{
    public interface RowMapper<T>
    {
        T MapRow(MySqlDataReader reader);
    }
}