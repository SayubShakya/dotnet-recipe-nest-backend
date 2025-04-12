// RoleRepositoryDatabaseImpl.cs

using RecipeNest.Consta;
using RecipeNest.Db;
using RecipeNest.Db.Query.Impl;
using RecipeNest.Dto;
using RecipeNest.Model;

namespace RecipeNest.Repository.Impl.Database;

public class RoleRepositoryDatabaseImpl : IRoleRepository
{
    
    public Paged<Role> GetAllPaginated(int start, int limit)
    {
        return DatabaseConnector.QueryAll(IQueryConstant.IRole.GetAllActiveOrderByCreatedDate, IQueryConstant.IRole.AllActiveCount, start, limit, new RoleRowMapper());
    }
    public bool DeleteById(int id)
    {
        var role = GetById(id);
        if (role == null) return false;

        DatabaseConnector.Update(IQueryConstant.IRole.DeleteById, id);
        return true;
    }
    
    public List<Role> GetAll()
    {
        return DatabaseConnector.QueryAll(IQueryConstant.IRole.GetAllActiveOrderByCreatedDate, new RoleRowMapper());
    }

    public Role GetById(int id)
    {
        return DatabaseConnector.QueryOne(IQueryConstant.IRole.GetById, new RoleRowMapper(), id);;
    }

    public bool Save(Role role)
    {
        if (role == null) return false;

        DatabaseConnector.Update(IQueryConstant.IRole.Save, role.Name);
        return true;
    }

    public bool Update(Role role)
    {
        if (role == null) return false;

        DatabaseConnector.Update(IQueryConstant.IRole.Update, role.Name, role.Id);
        return true;
    }
}