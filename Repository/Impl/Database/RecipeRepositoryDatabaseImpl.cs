// Repository/Impl/Database/RecipeRepositoryDatabaseImpl.cs

using RecipeNest.Constant;
using RecipeNest.Db;
using RecipeNest.Db.Query.Impl.Entity;
using RecipeNest.Db.Query.Impl.Projection;
using RecipeNest.Dto;
using RecipeNest.Entity;
using RecipeNest.Projection;

namespace RecipeNest.Repository.Impl.Database;

public class RecipeRepositoryDatabaseImpl : IRecipeRepository
{
    public Paged<Recipe> GetAllPaginated(int start, int limit)
    {
        return DatabaseConnector.QueryAll(IQueryConstant.IRecipe.GetAllActiveOrderByCreatedDate,
            IQueryConstant.IRecipe.AllActiveCount, start, limit, new RecipeRowMapper());
    }

    public Paged<RecipeProjection> GetAllAuthorizedPaginated(int start, int limit, int userId)
    {
        return DatabaseConnector.QueryAllWithParams(IQueryConstant.IRecipe.GetAllActiveAuthorized,
            IQueryConstant.IRecipe.GetAllActiveAuthorizedCount, start, limit, new RecipeProjectionRowMapper(), userId, userId);
    }


    public Paged<Recipe> GetFavoriteRecipes(int userId, int start, int limit)
    {
        return DatabaseConnector.QueryAllWithParams(IQueryConstant.IRecipe.GetAllFavorites,
            IQueryConstant.IRecipe.CountAllFavorites, start, limit, new RecipeRowMapper(), userId);
    }


    public bool DeleteById(int id)
    {
        var recipe = GetActiveById(id);
        if (recipe == null) return false;

        DatabaseConnector.Update(IQueryConstant.IRecipe.DeleteById, id);
        return true;
    }

    public List<Recipe> GetAll()
    {
        return DatabaseConnector.QueryAll(IQueryConstant.IRecipe.GetAllActiveOrderByCreatedDate, new RecipeRowMapper());
    }

    public Recipe GetActiveById(int id)
    {
        return DatabaseConnector.QueryOne(IQueryConstant.IRecipe.GetById, new RecipeRowMapper(), id);
    }

    public Recipe GetByTitle(string title)
    {
        return DatabaseConnector.QueryOne(IQueryConstant.IRecipe.GetByTitle, new RecipeRowMapper(), title);
    }


    public bool Save(Recipe recipe)
    {
        if (recipe == null) return false;

        DatabaseConnector.Update(IQueryConstant.IRecipe.Save,
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

        DatabaseConnector.Update(IQueryConstant.IRecipe.Update,
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