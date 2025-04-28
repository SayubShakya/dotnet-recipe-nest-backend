using RecipeNest.Dto;
using RecipeNest.Entity;
using RecipeNest.Projection;
using RecipeNest.Response;

namespace RecipeNest.Repository;

public interface IUserRepository : IBaseRepository<User>
{
    User? GetByEmail(string email);

    UserDetailProjection? GetUserDetailProjectionById(int id);

    Paged<UserTableProjection> GetAllPaginated(int start, int limit);

    bool RestoreById(int id);

    User GetInactiveById(int id);

    User GetById(int id);

    bool UpdateProfile(User user);

    Paged<ChefTableProjection> GetAllActiveChef(int start, int limit);
}