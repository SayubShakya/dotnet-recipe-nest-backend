using MySql.Data.MySqlClient;
using RecipeNest.Db.Query;
using RecipeNest.Dto;

namespace RecipeNest.Db;

public class DatabaseConnector
{
    
    private static readonly string ConnectionString =
        $"Server={Environment.GetEnvironmentVariable("DATABASE_URL")};Database={Environment.GetEnvironmentVariable("DATABASE_NAME")};User ID={Environment.GetEnvironmentVariable("DATABASE_USERNAME")};Password={Environment.GetEnvironmentVariable("DATABASE_PASSWORD")};Pooling=true;MinPoolSize=100;MaxPoolSize=300;";

    private static MySqlConnection GetConnection()
    {
        var connection = new MySqlConnection(ConnectionString);
        connection.Open();
        return connection;
    }

    private static void Disconnect(MySqlConnection connection)
    {
        if (connection != null) connection.Close();
    }

    public static int Update(string sql, params object[] parameters)
    {
        MySqlConnection? sqlConnection = null;
        MySqlCommand? sqlCommand = null;

        try
        {
            Console.WriteLine(sql);
            sqlConnection = GetConnection();
            sqlCommand = GetPreparedStatement(sqlConnection, sql);

            MapParams(parameters, sqlCommand);
            
            

            return sqlCommand.ExecuteNonQuery();
        }
        catch (Exception exception)
        {
            Console.WriteLine($"######## Error updating query: {sql}");
            Console.WriteLine(exception.Message);
            throw;
        }
        finally
        {
            Close(sqlConnection, sqlCommand);
        }

        return 0;
    }

    public static List<T> QueryAll<T>(string sql, IRowMapper<T> rowMapper)
    {
        List<T> dataList = [];
        MySqlConnection? sqlConnection = null;
        try
        {
            sqlConnection = GetConnection();
            MySqlCommand command = new(sql, sqlConnection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var data = rowMapper.Map(reader);
                dataList.Add(data);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
            throw;
        }
        finally
        {
            Disconnect(sqlConnection);
        }

        return dataList;
    }


    public static T QueryOne<T>(string sql, IRowMapper<T> rowMapper, params object[] parameters)
    {
        var sqlConnection = GetConnection();
        var preparedStatement = GetPreparedStatement(sqlConnection, sql);

        try
        {
            MapParams(parameters, preparedStatement);

            using (var resultSet = preparedStatement.ExecuteReader())
            {
                if (resultSet.Read()) return rowMapper.Map(resultSet);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"######## Error fetching: {sql}");
            Console.WriteLine(ex.Message);
            throw;
        }
        finally
        {
            Disconnect(sqlConnection);
        }


        return default;
    }


    public static List<T> QueryList<T>(string sql, IRowMapper<T> rowMapper, params object[] parameters)
    {
        List<T> results = new List<T>();
        var sqlConnection = GetConnection();
        var preparedStatement = GetPreparedStatement(sqlConnection, sql);

        try
        {
            if (parameters != null && parameters.Length > 0) MapParams(parameters, preparedStatement);

            using (var resultSet = preparedStatement.ExecuteReader())
            {
                while (resultSet.Read()) results.Add(rowMapper.Map(resultSet));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"######## Error fetching list: {sql}");
            Console.WriteLine(ex.Message);
            throw;
        }
        finally
        {
            Disconnect(sqlConnection);
        }

        return results;
    }


    public static int Count(string sql)
    {
        MySqlConnection? sqlConnection = null;
        try
        {
            sqlConnection = GetConnection();
            MySqlCommand command = new(sql, sqlConnection);
            var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return reader.GetInt32(reader.GetOrdinal("count"));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
            throw;
        }
        finally
        {
            Disconnect(sqlConnection);
        }

        return -1;
    }

    public static Paged<T> QueryAll<T>(string sql, string countSql, int start, int limit, IRowMapper<T> rowMapper)
    {
        int count = 0;
        int offset = (start - 1) * limit;
        sql += $" LIMIT {limit} OFFSET {offset}";

        Console.WriteLine(sql);
        List<T> dataList = [];
        MySqlConnection? sqlConnection = null;
        try
        {
            sqlConnection = GetConnection();
            MySqlCommand command = new(sql, sqlConnection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var data = rowMapper.Map(reader);
                dataList.Add(data);
            }

            count = Count(countSql);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
            throw;
        }
        finally
        {
            Disconnect(sqlConnection);
        }

        return new Paged<T>(start, limit, count, dataList);
    }


    public static Paged<T> QueryAllWithParams<T>(string sql, string countSql, int start, int limit,
        IRowMapper<T> rowMapper, params object[] parameters)
    {
        int count = 0;
        int offset = (start - 1) * limit;
        sql += $" LIMIT {limit} OFFSET {offset}";

        Console.WriteLine(sql);
        List<T> dataList = [];
        MySqlConnection? sqlConnection = null;
        MySqlCommand? command = null;

        try
        {
            sqlConnection = GetConnection();
            command = GetPreparedStatement(sqlConnection, sql);

            MapParams(parameters, command);

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var data = rowMapper.Map(reader);
                dataList.Add(data);
            }

            sqlConnection = GetConnection();
            Console.WriteLine(countSql);
            command = GetPreparedStatement(sqlConnection, countSql);
            MapParams(parameters, command);
            count = Convert.ToInt32(command.ExecuteScalar());
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
            throw;
        }
        finally
        {
            Disconnect(sqlConnection);
        }

        return new Paged<T>(start, limit, count, dataList);
    }


    private static void MapParams(object[] parameters, MySqlCommand mySqlCommand)
    {
        var counter = 1;
        foreach (var param in parameters) mySqlCommand.Parameters.AddWithValue("@param" + counter++, param);
    }

    private static MySqlCommand GetPreparedStatement(MySqlConnection connection, string sql)
    {
        return new MySqlCommand(sql, connection);
    }

    private static void Close(MySqlConnection? connection, MySqlCommand? command)
    {
        command?.Dispose();
        connection?.Close();
    }
}