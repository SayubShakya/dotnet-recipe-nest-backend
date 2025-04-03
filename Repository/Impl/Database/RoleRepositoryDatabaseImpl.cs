// RoleRepositoryDatabaseImpl.cs

using System.Collections.Generic;
using RecipeNest.Model;
using RecipeNest.Db;
using RecipeNest.Consta;
using RecipeNest.Db.Query.Impl;

namespace RecipeNest.Repository.Impl.Database
{

    public class RoleRepositoryDatabaseImpl : IRoleRepository
    {
        private DatabaseConnector databaseConnector = new DatabaseConnector();

        public bool DeleteById(int id)
        {
            Role role = GetById(id);
            if (role == null)
            {
                return false;
            }
            DatabaseConnector.Update(IQueryConstant.IRole.DELETE_BY_ID, id);
            return true;
        }

        public List<Role> GetAll()
        {
            return databaseConnector.QueryAll(IQueryConstant.IRole.GET_ALL, new RoleRowMapper());
        }

        public Role GetById(int id)
        {
            Role role;

            role = DatabaseConnector.QueryOne(IQueryConstant.IRole.GET_BY_ID, new RoleRowMapper(), id);

            return role;
        }

        public bool Save(Role role)
        {
            if (role == null)
            {
                return false;
            }
            DatabaseConnector.Update(IQueryConstant.IRole.SAVE, [role.Name]);
            return true;
        }

        public bool Update(Role role)
        {
            if (role == null)
            {
                return false;
            }

            DatabaseConnector.Update(IQueryConstant.IRole.UPDATE, [ role.Name, role.Id,]);
            return true;
        }
    }
}