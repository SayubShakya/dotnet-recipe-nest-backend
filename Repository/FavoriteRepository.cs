using RecipeNest.Model;

namespace RecipeNest.Repository;

public interface IFavoriteRepository : IBaseRepository<Favorite>
{
    Favorite GetByUserAndRecipe(int userId, int recipeId);
    bool DeleteByUserAndRecipe(int userId, int recipeId);
}