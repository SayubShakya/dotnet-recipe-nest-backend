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
        return DatabaseConnector.QueryAll(IQueryConstant.IUser.GetAllActiveOrderByCreatedDate, IQueryConstant.IUser.AllActiveCount, start, limit, new UserRowMapper());
    }

    public bool DeleteById(int id)
    {
        var user = GetById(id);
        if (user == null) return false;

        DatabaseConnector.Update(IQueryConstant.IUser.DeleteById, id);
        return true;
    }

    public List<User> GetAll()
    {
        return DatabaseConnector.QueryAll(IQueryConstant.IUser.GetAllActiveOrderByCreatedDate, new UserRowMapper());
    }


    public User GetByEmail(string email)
    {
        return DatabaseConnector.QueryOne(IQueryConstant.IUser.GetByEmail, new UserRowMapper(), email);
    }

    public User GetById(int id)
    {
        return DatabaseConnector.QueryOne(IQueryConstant.IUser.GetById, new UserRowMapper(), id);
    }

    public bool Save(User user)
    {
        if (user == null) return false;

        DatabaseConnector.Update(IQueryConstant.IUser.Save,
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

        DatabaseConnector.Update(IQueryConstant.IUser.Update,
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