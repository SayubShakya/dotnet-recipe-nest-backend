using System.Collections.Generic;
using RecipeNest.Model;
using RecipeNest.Db;
using RecipeNest.Consta; // Correct namespace
using RecipeNest.Db.Query.Impl; // Assuming FavoriteRowMapper is here
using System; 

namespace RecipeNest.Repository.Impl.Database
{
    public class FavoriteRepositoryDatabaseImpl : IFavoriteRepository
    {
        private static readonly DatabaseConnector databaseConnector = new DatabaseConnector();

        public bool Save(Favorite favorite)
        {
            if (favorite == null) return false;

            try
            {
                var existing = GetByUserAndRecipe(favorite.UserId, favorite.RecipeId);
                if (existing != null)
                {
                    Console.WriteLine($"Favorite already exists for UserID: {favorite.UserId}, RecipeID: {favorite.RecipeId}");
                    return false; 
                }

                int rowsAffected = DatabaseConnector.Update(IQueryConstant.IFavorite.SAVE,
                    favorite.UserId,
                    favorite.RecipeId);
                return rowsAffected > 0;
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error saving favorite: {ex.Message}");
                return false;
            }
        }


        public Favorite GetByUserAndRecipe(int userId, int recipeId)
        {
            try
            {
                return DatabaseConnector.QueryOne(
                    IQueryConstant.IFavorite.GET_BY_ID,
                    new FavoriteRowMapper(), 
                    userId,
                    recipeId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting favorite by user/recipe: {ex.Message}");
                return null;
            }
        }

        public bool DeleteByUserAndRecipe(int userId, int recipeId)
        {
            try
            {
                return DatabaseConnector.Update(
                    IQueryConstant.IFavorite.DELETE_BY_ID,
                    userId,
                    recipeId) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting favorite by user/recipe: {ex.Message}");
                return false;
            }
        }

        public bool Update(Favorite obj)
        {
            throw new NotImplementedException("Favorites are typically not updated directly, but deleted and re-added if necessary. Use specific methods.");
        }

        public Favorite GetById(int id)
        {
            throw new NotImplementedException("Use GetByUserAndRecipe instead. Favorites are identified by UserID and RecipeID.");
        }


        public List<Favorite> GetAll()
        {
            throw new NotImplementedException("Use specific methods like GetByUserAndRecipe or potentially GetAllByUserId.");
        }

        public bool DeleteById(int id)
        {
            throw new NotImplementedException("Use DeleteByUserAndRecipe instead. Favorites are identified by UserID and RecipeID.");
        }
    }
}