using RecipeNest.Constant;
using RecipeNest.Db;
using RecipeNest.Db.Query.Impl.Entity;
using RecipeNest.Db.Query.Impl.Projection;
using RecipeNest.Dto;
using RecipeNest.Entity;
using RecipeNest.Projection;
using RecipeNest.Response;

namespace RecipeNest.Repository.Impl.Database;

public class UserRepositoryDatabaseImpl : IUserRepository
{
    public Paged<UserTableProjection> GetAllPaginated(int start, int limit)
    {
        return DatabaseConnector.QueryAll(IQueryConstant.IUser.GetUsersWithRoles,
            IQueryConstant.IUser.GetUsersWithRolesCount,
            start, limit, new UserTableProjectionRowMapper());
    }

    public Paged<ChefTableProjection> GetAllActiveChef(int start, int limit)
    {
        return DatabaseConnector.QueryAll(IQueryConstant.IUser.GetActiveChefs, IQueryConstant.IUser.GetActiveChefsCount,
            start, limit, new ChefTableProjectionRowMapper());
    }


    public bool DeleteById(int id)
    {
        var user = GetActiveById(id);
        DatabaseConnector.Update(IQueryConstant.IUser.DeleteById, id);
        return true;
    }

    public List<User> GetAll()
    {
        return DatabaseConnector.QueryAll(IQueryConstant.IUser.GetAllActiveOrderByCreatedDate, new UserRowMapper());
    }


    public User? GetByEmail(string email)
    {
        return DatabaseConnector.QueryOne(IQueryConstant.IUser.GetByEmail,
            new UserRowMapper(), email);
    }

    public UserDetailProjection? GetUserDetailProjectionById(int id)
    {
        return DatabaseConnector.QueryOne(IQueryConstant.IUser.GetUserDetailByIdProjection,
            new UserDetailProjectionRowMapper(), id);
    }

    public User GetActiveById(int id)
    {
        return DatabaseConnector.QueryOne(IQueryConstant.IUser.GetActiveById, new UserRowMapper(), id);
    }

    public User GetById(int id)
    {
        return DatabaseConnector.QueryOne(IQueryConstant.IUser.GetById, new UserRowMapper(), id);
    }

    public User GetInactiveById(int id)
    {
        return DatabaseConnector.QueryOne(IQueryConstant.IUser.GetInactiveById, new UserRowMapper(), id);
    }

    public bool Save(User user)
    {
        return DatabaseConnector.Update(IQueryConstant.IUser.Save,
            user.FirstName,
            user.LastName,
            user.PhoneNumber,
            user.ImageUrl!,
            user.About!,
            user.Email,
            user.Password,
            user.RoleId
        ) == 1;
    }

    public bool Update(User user)
    {
        return DatabaseConnector.Update(IQueryConstant.IUser.Update,
            user.FirstName,
            user.LastName,
            user.PhoneNumber,
            user.ImageUrl!,
            user.About!,
            user.Email,
            user.Password,
            user.RoleId,
            user.Id
        ) == 1;
    }

    public bool UpdateProfile(User user)
    {
        return DatabaseConnector.Update(IQueryConstant.IUser.UpdateProfile,
            user.FirstName,
            user.LastName,
            user.PhoneNumber,
            user.ImageUrl!,
            user.About!,
            user.Id
        ) == 1;
    }

    public bool RestoreById(int id)
    {
        var user = GetActiveById(id);
        DatabaseConnector.Update(IQueryConstant.IUser.RestoreById, id);
        return true;
    }
}