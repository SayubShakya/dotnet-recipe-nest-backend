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
        return DatabaseConnector.QueryAll(IQueryConstant.IRole.GET_ALL_ACTIVE_ORDER_BY_CREATED_DATE, IQueryConstant.IRole.ALL_ACTIVE_COUNT, start, limit, new RoleRowMapper());
    }
    public bool DeleteById(int id)
    {
        var role = GetById(id);
        if (role == null) return false;

        DatabaseConnector.Update(IQueryConstant.IRole.DELETE_BY_ID, id);
        return true;
    }
    
    public List<Role> GetAll()
    {
        return DatabaseConnector.QueryAll(IQueryConstant.IRole.GET_ALL_ACTIVE_ORDER_BY_CREATED_DATE, new RoleRowMapper());
    }

    public Role GetById(int id)
    {
        Role role;
        role = DatabaseConnector.QueryOne(IQueryConstant.IRole.GetById, new RoleRowMapper(), id);
        return role;
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