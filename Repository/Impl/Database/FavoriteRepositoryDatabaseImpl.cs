using RecipeNest.Constant;
using RecipeNest.Db;
using RecipeNest.Db.Query.Impl.Entity;
using RecipeNest.Db.Query.Impl.Projection;
using RecipeNest.Dto;
using RecipeNest.Entity;
using RecipeNest.Projection;

namespace RecipeNest.Repository.Impl.Database;

public class FavoriteRepositoryDatabaseImpl : IFavoriteRepository
{
    public bool Save(Favorite favorite)
    {
        if (favorite == null) return false;

        try
        {
            var existing = GetByUserAndRecipe(favorite.UserId, favorite.RecipeId);
            if (existing != null)
            {
                Console.WriteLine(
                    $"Favorite already exists for UserID: {favorite.UserId}, RecipeID: {favorite.RecipeId}");
                return false;
            }

            DatabaseConnector.Update(IQueryConstant.IFavorite.Save, favorite.UserId, favorite.RecipeId);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving favorite: {ex.Message}");
            return false;
        }
    }
    
    public Paged<RecipeProjection> GetAllAuthorizedPaginated(int start, int limit, int userId)
    {
        return DatabaseConnector.QueryAllWithParams(IQueryConstant.IRecipe.GetAllFavorites,
            IQueryConstant.IRecipe.CountAllFavorites, start, limit, new FavoriteRecipeProjectionRowMapper(), userId, userId);
    }


    public Favorite GetByUserAndRecipe(int userId, int recipeId)
    {
        try
        {
            return DatabaseConnector.QueryOne(IQueryConstant.IFavorite.GetById, new FavoriteRowMapper(), userId,
                recipeId);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting favorite by user/recipe: {ex.Message}");
            return null;
        }
    }

    public bool Update(Favorite obj)
    {
        throw new NotImplementedException(
            "Favorites are typically not updated directly, but deleted and re-added if necessary. Use specific methods.");
    }

    public Favorite GetActiveById(int id)
    {
        throw new NotImplementedException(
            "Use GetByUserAndRecipe instead. Favorites are identified by UserID and RecipeID.");
    }


    public List<Favorite> GetAll()
    {
        throw new NotImplementedException(
            "Use specific methods like GetByUserAndRecipe or potentially GetAllByUserId.");
    }

    public bool DeleteById(int id)
    {
        try
        {
            return DatabaseConnector.Update(IQueryConstant.IFavorite.DeleteById, id) > 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting favorite: {ex.Message}");
            return false;
        }
    }
}