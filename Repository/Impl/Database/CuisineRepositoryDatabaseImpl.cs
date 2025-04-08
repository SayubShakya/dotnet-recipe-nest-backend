// CuisineRepositoryDatabaseImpl.cs

using RecipeNest.Consta;
using RecipeNest.Db;
using RecipeNest.Db.Query.Impl;
using RecipeNest.Dto;
using RecipeNest.Model;

// For IQueryConstant
// For DatabaseConnector
// For CuisineRowMapper
// For Cuisine model

namespace RecipeNest.Repository.Impl.Database;

public class CuisineRepositoryDatabaseImpl : ICuisineRepository
{

    public  Paged<Cuisine> GetAllPaginated(int start, int limit)
    {
        return DatabaseConnector.QueryAll(IQueryConstant.ICuisine.GET_ALL_ACTIVE_ORDER_BY_CREATED_DATE, IQueryConstant.ICuisine.ALL_ACTIVE_COUNT, start, limit, new CuisineRowMapper());
    }

    public bool DeleteById(int id)
    {
        var cuisine = GetById(id);
        if (cuisine == null) return false;

        DatabaseConnector.Update(IQueryConstant.ICuisine.DELETE_BY_ID, id);
        return true;
    }

    public List<Cuisine> GetAll()
    {
        return DatabaseConnector.QueryAll(IQueryConstant.ICuisine.GET_ALL_ACTIVE_ORDER_BY_CREATED_DATE, new CuisineRowMapper());
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
        if (cuisine == null) return false;

        DatabaseConnector.Update(IQueryConstant.ICuisine.SAVE, cuisine.Name, cuisine.ImageUrl);
        return true;
    }

    public bool Update(Cuisine cuisine)
    {
        if (cuisine == null) return false;

        DatabaseConnector.Update(IQueryConstant.ICuisine.UPDATE, cuisine.Name, cuisine.ImageUrl, cuisine.Id);
        return true;
    }
}