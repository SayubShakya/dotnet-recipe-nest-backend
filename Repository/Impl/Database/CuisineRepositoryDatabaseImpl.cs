// CuisineRepositoryDatabaseImpl.cs

using RecipeNest.Consta;
using RecipeNest.Db;
using RecipeNest.Db.Query.Impl;
using RecipeNest.Dto;
using RecipeNest.Model;

namespace RecipeNest.Repository.Impl.Database;

public class CuisineRepositoryDatabaseImpl : ICuisineRepository
{

    public Paged<Cuisine> GetAllPaginated(int start, int limit)
    {
        return DatabaseConnector.QueryAll(IQueryConstant.ICuisine.GetAllActiveOrderByCreatedDate, IQueryConstant.ICuisine.AllActiveCount, start, limit, new CuisineRowMapper());
    }

    public bool DeleteById(int id)
    {
        var cuisine = GetById(id);
        if (cuisine == null) return false;

        DatabaseConnector.Update(IQueryConstant.ICuisine.DeleteById, id);
        return true;
    }

    public List<Cuisine> GetAll()
    {
        return DatabaseConnector.QueryAll(IQueryConstant.ICuisine.GetAllActiveOrderByCreatedDate, new CuisineRowMapper());
    }

    public Cuisine GetById(int id)
    {
        return DatabaseConnector.QueryOne(IQueryConstant.ICuisine.GetById, new CuisineRowMapper(), id);
    }

    public Cuisine GetByName(string name)
    {
        return DatabaseConnector.QueryOne(IQueryConstant.ICuisine.GetByName, new CuisineRowMapper(), name);;
    }


    public bool Save(Cuisine cuisine)
    {
        if (cuisine == null) return false;

        DatabaseConnector.Update(IQueryConstant.ICuisine.Save, cuisine.Name, cuisine.ImageUrl);
        return true;
    }

    public bool Update(Cuisine cuisine)
    {
        if (cuisine == null) return false;

        DatabaseConnector.Update(IQueryConstant.ICuisine.Update, cuisine.Name, cuisine.ImageUrl, cuisine.Id);
        return true;
    }
}