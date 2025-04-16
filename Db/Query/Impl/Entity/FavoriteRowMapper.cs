﻿// FavoriteRowMapper.cs

using MySql.Data.MySqlClient;
using RecipeNest.Entity;

namespace RecipeNest.Db.Query.Impl.Entity;

public class FavoriteRowMapper : IRowMapper<Favorite>
{
    public Favorite Map(MySqlDataReader reader)
    {
        var id = reader.GetInt32("id");
        var recipeId = reader.GetInt32("recipe_id");
        var userId = reader.GetInt32("user_id");
        return new Favorite(id, recipeId, userId);
    }
}