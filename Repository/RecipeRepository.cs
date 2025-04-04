// Repository/IRecipeRepository.cs
using System.Collections.Generic; 
using RecipeNest.Model; 

namespace RecipeNest.Repository
{
    public interface IRecipeRepository : IBaseRepository<Recipe>
    {
        Recipe? GetByTitle(string title);
    }
}