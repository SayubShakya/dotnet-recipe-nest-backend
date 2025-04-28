using RecipeNest.Constant;
using RecipeNest.Db;
using RecipeNest.Db.Query.Impl.Projection;
using RecipeNest.Dto;
using RecipeNest.Entity;
using RecipeNest.Projection;

namespace RecipeNest.Repository;

public interface IFavoriteRepository : IBaseRepository<Favorite>
{
    Favorite GetByUserAndRecipe(int userId, int recipeId);
    
    public Paged<RecipeProjection> GetAllAuthorizedPaginated(int start, int limit, int userId)
    {
        return DatabaseConnector.QueryAllWithParams(IQueryConstant.IRecipe.GetAllFavorites,
            IQueryConstant.IRecipe.CountAllFavorites, start, limit, new RecipeProjectionRowMapper(), userId, userId);
    }
}