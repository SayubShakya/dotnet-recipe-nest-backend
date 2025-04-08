// Repository/IRecipeRepository.cs

using RecipeNest.Dto;
using RecipeNest.Model;

namespace RecipeNest.Repository;

public interface IRecipeRepository : IBaseRepository<Recipe>
{
    Recipe? GetByTitle(string title);
    Paged<Recipe> GetAllPaginated(int start, int limit);

}