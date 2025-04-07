// CuisineRepositoryDatabaseImpl.cs
using RecipeNest.Consta; // For IQueryConstant
using RecipeNest.Db; // For DatabaseConnector
using RecipeNest.Db.Query.Impl; // For CuisineRowMapper
using RecipeNest.Model; // For Cuisine model
using System.Collections.Generic;

namespace RecipeNest.Repository.Impl.Database
{

    public class CuisineRepositoryDatabaseImpl : ICuisineRepository
    {
        private DatabaseConnector databaseConnector = new DatabaseConnector();
        public bool DeleteById(int id)
        {
            Cuisine cuisine = GetById(id);
            if (cuisine == null)
            {
                return false;
            }

            DatabaseConnector.Update(IQueryConstant.ICuisine.DELETE_BY_ID, id);
            return true;
        }

        public List<Cuisine> GetAll()
        {
            return databaseConnector.QueryAll(IQueryConstant.ICuisine.GET_ALL, new CuisineRowMapper());

        }

        public Cuisine GetById(int id)
        {
            Cuisine cuisine;
            cuisine = DatabaseConnector.QueryOne(IQueryConstant.ICuisine.GET_BY_ID, new CuisineRowMapper(), id);
            return cuisine;
        }
        
        public Cuisine GetByName(string name)
        {
            Cuisine cuisine;
            cuisine = DatabaseConnector.QueryOne(IQueryConstant.ICuisine.GET_BY_NAME, new CuisineRowMapper(), name);
            return cuisine;
        }


        public bool Save(Cuisine cuisine)
        {
            if (cuisine == null)
            {
                return false;
            }

            DatabaseConnector.Update(IQueryConstant.ICuisine.SAVE, cuisine.Name, cuisine.ImageUrl);
            return true;
        }

        public bool Update(Cuisine cuisine)
        {
            if (cuisine == null)
            {
                return false;
            }

            DatabaseConnector.Update(IQueryConstant.ICuisine.UPDATE, cuisine.Name, cuisine.ImageUrl, cuisine.Id);
            return true;
        }
    }
}