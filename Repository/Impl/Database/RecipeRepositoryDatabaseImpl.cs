// Repository/Impl/Database/RecipeRepositoryDatabaseImpl.cs

using RecipeNest.Consta;
using RecipeNest.Db;
using RecipeNest.Db.Query.Impl;
using RecipeNest.Dto;
using RecipeNest.Model;

namespace RecipeNest.Repository.Impl.Database;

public class RecipeRepositoryDatabaseImpl : IRecipeRepository
{

    public Paged<Recipe> GetAllPaginated(int start, int limit)
    {
        return DatabaseConnector.QueryAll(IQueryConstant.IRecipe.GET_ALL_ACTIVE_ORDER_BY_CREATED_DATE, IQueryConstant.IRecipe.ALL_ACTIVE_COUNT, start, limit, new RecipeRowMapper());
    }
    public bool DeleteById(int id)
    {
        var recipe = GetById(id);
        if (recipe == null) return false;

        DatabaseConnector.Update(IQueryConstant.IRecipe.DELETE_BY_ID, id);
        return true;
    }

    public List<Recipe> GetAll()
    {
        return DatabaseConnector.QueryAll(IQueryConstant.IRecipe.GET_ALL_ACTIVE_ORDER_BY_CREATED_DATE, new RecipeRowMapper());
    }

    public Recipe GetById(int id)
    {
        return DatabaseConnector.QueryOne(IQueryConstant.IRecipe.GET_BY_ID, new RecipeRowMapper(), id);
    }

    public Recipe GetByTitle(string title)
    {
        return DatabaseConnector.QueryOne(IQueryConstant.IRecipe.GET_BY_TITLE, new RecipeRowMapper(), title);
    }


    public bool Save(Recipe recipe)
    {
        if (recipe == null) return false;

        DatabaseConnector.Update(IQueryConstant.IRecipe.SAVE,
            recipe.ImageUrl,
            recipe.Title,
            recipe.Description,
            recipe.RecipeDetail,
            recipe.Ingredients,
            recipe.RecipeByUserId,
            recipe.CuisineId
        );
        return true;
    }

    public bool Update(Recipe recipe)
    {
        if (recipe == null) return false;

        DatabaseConnector.Update(IQueryConstant.IRecipe.UPDATE,
            recipe.ImageUrl,
            recipe.Title,
            recipe.Description,
            recipe.RecipeDetail,
            recipe.Ingredients,
            recipe.RecipeByUserId,
            recipe.CuisineId,
            recipe.Id
        );
        return true;
    }
}