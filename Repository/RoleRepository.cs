// RoleRepository.cs

using RecipeNest.Dto;
using RecipeNest.Entity;

namespace RecipeNest.Repository;

public interface IRoleRepository : IBaseRepository<Role>
{
    Paged<Role> GetAllPaginated(int start, int limit);
    
}