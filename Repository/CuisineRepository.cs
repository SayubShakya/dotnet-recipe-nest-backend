// RoleRepository.cs

using RecipeNest.Model;

namespace RecipeNest.Repository
{

    public interface ICuisineRepository : IBaseRepository<Cuisine>
    {
        Cuisine? GetByName(string name); 

    }
}