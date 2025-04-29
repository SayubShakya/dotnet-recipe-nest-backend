// Repository/IRecipeRepository.cs

using RecipeNest.Dto;
using RecipeNest.Entity;
using RecipeNest.Projection;

namespace RecipeNest.Repository;

public interface IRecipeRepository : IBaseRepository<Recipe>
{
    Recipe? GetByTitle(string title);
    Paged<RecipeProjection> GetAllPaginated(int start, int limit);
    Paged<RecipeProjection> GetAllAuthorizedPaginated(int start, int limit, int userId);
    Paged<RecipeProjection> GetAllByChefPaginated(int start, int limit, int userId);
    Paged<Recipe> GetFavoriteRecipes(int userId, int start, int limit);
    
}