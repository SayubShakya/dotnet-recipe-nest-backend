using MySql.Data.MySqlClient;

namespace RecipeNest.Db.Query;

public interface IRowMapper<out T>
{
    T Map(MySqlDataReader reader);
}