using RecipeNest.Model;

namespace RecipeNest.Repository;

public interface IUserRepository : IBaseRepository<User>
{
    User? GetByEmail(string email);
}