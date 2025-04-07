using System.Collections.Generic;
using RecipeNest.Model;

namespace RecipeNest.Repository
{
    public interface IRatingRepository : IBaseRepository<Rating>
    {
        Rating GetByUserAndRecipe(int userId, int recipeId);
        bool DeleteByUserAndRecipe(int userId, int recipeId);
    }
}