using RecipeNest.Dto;
using RecipeNest.Model;
using RecipeNest.Response;

namespace RecipeNest.Repository;

public interface IUserRoleRepository: IBaseRepository<UserRoleResponse>
{
    Paged<User> GetAllWithRolesPaginated(int start, int limit);
}