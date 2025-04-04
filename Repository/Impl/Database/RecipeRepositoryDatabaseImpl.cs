// Repository/Impl/Database/RecipeRepositoryDatabaseImpl.cs
using System.Collections.Generic;
using RecipeNest.Model;
using RecipeNest.Db;
using RecipeNest.Consta;
using RecipeNest.Db.Query.Impl;

namespace RecipeNest.Repository.Impl.Database
{
    public class RecipeRepositoryDatabaseImpl : IRecipeRepository
    {
        private DatabaseConnector databaseConnector = new DatabaseConnector();

        public bool DeleteById(int id)
        {
            Recipe recipe = GetById(id);
            if (recipe == null)
            {
                return false;
            }

            DatabaseConnector.Update(IQueryConstant.IRecipe.DELETE_BY_ID, id);
            return true;
        }

        public List<Recipe> GetAll()
        {
            return databaseConnector.QueryAll(IQueryConstant.IRecipe.GET_ALL, new RecipeRowMapper());
        }

        public Recipe GetById(int id)
        {
            Recipe recipe;
            recipe = DatabaseConnector.QueryOne(IQueryConstant.IRecipe.GET_BY_ID, new RecipeRowMapper(), id);
            return recipe;
        }

        public Recipe GetByTitle(string title)
        {
            Recipe recipe;
            recipe = DatabaseConnector.QueryOne(IQueryConstant.IRecipe.GET_BY_TITLE, new RecipeRowMapper(), title);
            return recipe;
        }


        public bool Save(Recipe recipe)
        {
            if (recipe == null)
            {
                return false;
            }

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
            if (recipe == null)
            {
                return false;
            }

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
}