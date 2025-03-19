using Mapper;
using MySql.Data.MySqlClient;


namespace RecipeNest.Util.Database
{

    public class DatabaseUtil
    {
        private string connectionString = "Server=localhost;Database=recipe_nest;User ID=root;Password=9828807288;";

        private MySqlConnection? connection;


        private void Connect()
        {
            this.connection = new MySqlConnection(connectionString);
            this.connection.Open();
        }

        private void Disconnect()
        {

            if (this.connection?.State != System.Data.ConnectionState.Open)
            {
                this.connection?.Close();
            }

        }


        public List<T> Query<T>(string sql, RowMapper<T> rowMapper)
        {
            List<T> dataList = [];

            try
            {
                Connect();

                MySqlCommand command = new(sql, this.connection);

                MySqlDataReader reader = command.ExecuteReader();



                while (reader.Read())
                {

                    T data = rowMapper.MapRow(reader);

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
    }
}