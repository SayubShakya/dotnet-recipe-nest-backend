// RoleRepository.cs

using RecipeNest.Dto;
using RecipeNest.Model;

namespace RecipeNest.Repository;

public interface ICuisineRepository : IBaseRepository<Cuisine>
{
    Cuisine? GetByName(string name);
    
    Paged<Cuisine> GetAllPaginated(int start, int limit);
}