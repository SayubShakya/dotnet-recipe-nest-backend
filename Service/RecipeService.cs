// Service/RecipeService.cs

using System.Collections.Generic;
using System.Linq;
using RecipeNest.Model;
using RecipeNest.Reponse;
using RecipeNest.Repository;
using RecipeNest.Request;

namespace RecipeNest.Service
{
    public class RecipeService
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public List<RecipeResponse> GetAll()
        {
            List<Recipe> recipes = _recipeRepository.GetAll();
            return recipes.Select(recipe => new RecipeResponse(
                recipe.Id,
                recipe.ImageUrl,
                recipe.Title,
                recipe.Description,
                recipe.RecipeDetail,
                recipe.Ingredients,
                recipe.RecipeByUserId,
                recipe.CuisineId
            )).ToList();
        }

        public RecipeResponse? GetById(int id)
        {
            Recipe? recipe = _recipeRepository.GetById(id);
            if (recipe == null)
            {
                return null;
            }

            return new RecipeResponse(
                recipe.Id,
                recipe.ImageUrl,
                recipe.Title,
                recipe.Description,
                recipe.RecipeDetail,
                recipe.Ingredients,
                recipe.RecipeByUserId,
                recipe.CuisineId
            );
        }

        public RecipeResponse? GetByTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title)) return null;

            Recipe? recipe = _recipeRepository.GetByTitle(title);
            if (recipe == null)
            {
                return null;
            }

            return new RecipeResponse(
                recipe.Id,
                recipe.ImageUrl,
                recipe.Title,
                recipe.Description,
                recipe.RecipeDetail,
                recipe.Ingredients,
                recipe.RecipeByUserId,
                recipe.CuisineId
            );
        }

        public bool Save(CreateRecipeRequest request)
        {
            Recipe recipe = new Recipe
            {
                ImageUrl = request.ImageUrl,
                Title = request.Title,
                Description = request.Description,
                RecipeDetail = request.RecipeDetail,
                Ingredients = request.Ingredients,
                RecipeByUserId = request.RecipeByUserId,
                CuisineId = request.CuisineId
            };

            return _recipeRepository.Save(recipe);
        }

        public bool Update(UpdateRecipeRequest request)
        {
            Recipe? existingRecipe = _recipeRepository.GetById(request.Id);
            if (existingRecipe == null)
            {
                return false;
            }

            Recipe recipeToUpdate = new Recipe
            {
                Id = request.Id,
                ImageUrl = request.ImageUrl,
                Title = request.Title,
                Description = request.Description,
                RecipeDetail = request.RecipeDetail,
                Ingredients = request.Ingredients,
                RecipeByUserId = request.RecipeByUserId,
                CuisineId = request.CuisineId
            };

            return _recipeRepository.Update(recipeToUpdate);
        }

        public bool DeleteById(int id)
        {
            return _recipeRepository.DeleteById(id);
        }
    }
}