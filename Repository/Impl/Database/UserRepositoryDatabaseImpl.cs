// UserRepositoryDatabaseImpl.cs

using System.Collections.Generic;
using RecipeNest.Model;       
using RecipeNest.Db;           
using RecipeNest.Consta;   
using RecipeNest.Db.Query.Impl;

namespace RecipeNest.Repository.Impl.Database
{
    public class UserRepositoryDatabaseImpl : IUserRepository
    {
        private DatabaseConnector databaseConnector = new DatabaseConnector();

        public bool DeleteById(int id)
        {
            User user = GetById(id);
            if (user == null)
            {
            return false;
            }
            
            DatabaseConnector.Update(IQueryConstant.IUser.DELETE_BY_ID, id);
            return true; 
        }

        public List<User> GetAll()
        {
            return databaseConnector.QueryAll(IQueryConstant.IUser.GET_ALL, new UserRowMapper());
        }


        public User GetByEmail(string email)
        {
            User user;
            user = DatabaseConnector.QueryOne(IQueryConstant.IUser.GET_BY_EMAIL, new UserRowMapper(), email);
            return user;
        }
        
        public User GetById(int id)
        {
            User user;
            user = DatabaseConnector.QueryOne(IQueryConstant.IUser.GET_BY_ID, new UserRowMapper(), id);
            return user;
        }

        public bool Save(User user)
        {
            if (user == null)
            {
                return false;
            }
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
            if (user == null)
            {
                return false;
            }
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
}