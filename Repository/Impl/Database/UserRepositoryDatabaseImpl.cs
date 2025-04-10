// UserRepositoryDatabaseImpl.cs

using RecipeNest.Consta;
using RecipeNest.Db;
using RecipeNest.Db.Query.Impl;
using RecipeNest.Dto;
using RecipeNest.Model;

namespace RecipeNest.Repository.Impl.Database;

public class UserRepositoryDatabaseImpl : IUserRepository
{
    
    public Paged<User> GetAllPaginated(int start, int limit)
    {
        return DatabaseConnector.QueryAll(IQueryConstant.IUser.GET_ALL_ACTIVE_ORDER_BY_CREATED_DATE, IQueryConstant.IUser.ALL_ACTIVE_COUNT, start, limit, new UserRowMapper());
    }

    public bool DeleteById(int id)
    {
        var user = GetById(id);
        if (user == null) return false;

        DatabaseConnector.Update(IQueryConstant.IUser.DELETE_BY_ID, id);
        return true;
    }

    public List<User> GetAll()
    {
        return DatabaseConnector.QueryAll(IQueryConstant.IUser.GET_ALL_ACTIVE_ORDER_BY_CREATED_DATE, new UserRowMapper());
    }


    public User GetByEmail(string email)
    {
        return DatabaseConnector.QueryOne(IQueryConstant.IUser.GET_BY_EMAIL, new UserRowMapper(), email);
    }

    public User GetById(int id)
    {
        return DatabaseConnector.QueryOne(IQueryConstant.IUser.GET_BY_ID, new UserRowMapper(), id);
    }

    public bool Save(User user)
    {
        if (user == null) return false;

        DatabaseConnector.Update(IQueryConstant.IUser.SAVE,
            user.FirstName,
            user.LastName,
            user.PhoneNumber,
            user.ImageUrl,
            user.About,
            user.Email,
            user.Password,
            user.RoleId
        );
        return true;
    }

    public bool Update(User user)
    {
        if (user == null) return false;

        DatabaseConnector.Update(IQueryConstant.IUser.UPDATE,
            user.FirstName,
            user.LastName,
            user.PhoneNumber,
            user.ImageUrl,
            user.About,
            user.Email,
            user.Password,
            user.RoleId,
            user.Id
        );
        return true;
    }
}