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

    public User GetInactiveById(int id);

    public User GetById(int id);
}