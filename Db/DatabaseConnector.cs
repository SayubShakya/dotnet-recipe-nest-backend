// DatabaseConnector.cs

using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using RecipeNest.Db.Query;

namespace RecipeNest.Db;

public class DatabaseConnector
{
    private readonly string connectionString = "Server=localhost;Database=recipe_nest;User ID=root;Password=9828807288;";
    private MySqlConnection? connection;

    private void Connect()
    {
        this.connection = new MySqlConnection(connectionString);
        this.connection.Open();
    }

    private void Disconnect()
    {
        // if (this.connection?.State == System.Data.ConnectionState.Open)
        // {
        //     this.connection?.Close();
        // }
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

    public List<T> QueryAll<T>(string sql, IRowMapper<T> rowMapper)
    {
        List<T> dataList = [];

        try
        {
            Connect();
            MySqlCommand command = new(sql, this.connection);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                T data = rowMapper.Map(reader);
                dataList.Add(data);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
        finally
        {
            Disconnect();
        }

        return dataList;
    }


    public static T QueryOne<T>(string sql, IRowMapper<T> rowMapper, params object[] parameters)
    {
        using (var connection = GetConnection())
        using (var preparedStatement = GetPreparedStatement(connection, sql))
        {
            try
            {
                MapParams(parameters, preparedStatement);

                using (var resultSet = preparedStatement.ExecuteReader())
                {
                    if (resultSet.Read())
                    {
                        return rowMapper.Map(resultSet);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"######## Error fetching: {sql}");
                Console.WriteLine(ex.Message);
            }
        }
        return default(T);
    }

    private static void MapParams(object[] parameters, MySqlCommand mySqlCommand)
    {
        int counter = 1;
        foreach (var param in parameters)
        {
            mySqlCommand.Parameters.AddWithValue("@param"+(counter++), param);
        }
    }

    private static MySqlConnection GetConnection()
    {
        var connection = new MySqlConnection("Server=localhost;Database=recipe_nest;User ID=root;Password=9828807288;");
        connection.Open();
        return connection;
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