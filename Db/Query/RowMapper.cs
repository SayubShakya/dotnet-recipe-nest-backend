//RowMapper.cs

using MySql.Data.MySqlClient;

namespace RecipeNest.Db.Query;

public interface IRowMapper<T>
{
    T Map(MySqlDataReader reader);
}