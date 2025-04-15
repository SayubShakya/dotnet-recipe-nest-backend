// Repository/IRecipeRepository.cs

using RecipeNest.Dto;
using RecipeNest.Model;
using RecipeNest.Projection;

namespace RecipeNest.Repository;

public interface IRecipeRepository : IBaseRepository<Recipe>
{
    Recipe? GetByTitle(string title);
    Paged<Recipe> GetAllPaginated(int start, int limit);
    Paged<RecipeAuthorized> GetAllAuthorizedPaginated(int start, int limit,int userId);
    Paged<Recipe> GetFavoriteRecipes(int userId, int start, int limit);


}