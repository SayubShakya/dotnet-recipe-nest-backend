// CuisineRowMapper.cs

using MySql.Data.MySqlClient;
using RecipeNest.Model;
using RecipeNest.Db.Query;
using System;

namespace RecipeNest.Db.Query.Impl
{
    public class CuisineRowMapper : IRowMapper<Cuisine>
    {
        public Cuisine Map(MySqlDataReader reader)
        {
            int id = reader.GetInt32("id");
            string Name = reader.GetString("name");
            string ImageUrl = reader.GetString("image_url");
            return new Cuisine(id, Name, ImageUrl);
        }
    }
}