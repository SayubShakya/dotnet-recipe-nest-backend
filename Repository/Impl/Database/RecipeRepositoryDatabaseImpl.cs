// Repository/Impl/Database/RecipeRepositoryDatabaseImpl.cs

using RecipeNest.Consta;
using RecipeNest.Db;
using RecipeNest.Db.Query.Impl;
using RecipeNest.Model;

namespace RecipeNest.Repository.Impl.Database;

public class RecipeRepositoryDatabaseImpl : IRecipeRepository
{
    private readonly DatabaseConnector databaseConnector = new();

    public bool DeleteById(int id)
    {
        var recipe = GetById(id);
        if (recipe == null) return false;

        DatabaseConnector.Update(IQueryConstant.IRecipe.DELETE_BY_ID, id);
        return true;
    }

    public List<Recipe> GetAll()
    {
        return DatabaseConnector.QueryAll(IQueryConstant.IRecipe.GET_ALL, new RecipeRowMapper());
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