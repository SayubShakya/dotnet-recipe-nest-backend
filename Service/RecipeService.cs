// Service/RecipeService.cs

using RecipeNest.Dto;
using RecipeNest.Model;
using RecipeNest.Repository;
using RecipeNest.Request;
using RecipeNest.Response;

namespace RecipeNest.Service;

public class RecipeService
{
    private readonly IRecipeRepository _recipeRepository;

    public RecipeService(IRecipeRepository recipeRepository)
    {
        _recipeRepository = recipeRepository;
    }
    
    public PaginatedResponse<RecipeResponse> GetAll(int start, int limit)
    {
        Paged<Recipe> pagedRecipes = _recipeRepository.GetAllPaginated(start, limit);
        
        List<RecipeResponse> items =  pagedRecipes.Items.Select(recipe => new RecipeResponse(
            recipe.Id,
            recipe.ImageUrl,
            recipe.Title,
            recipe.Description,
            recipe.RecipeDetail,
            recipe.Ingredients,
            recipe.RecipeByUserId,
            recipe.CuisineId
        )).ToList();

        PaginatedResponse<RecipeResponse> paginatedResponse = new()
        {
            Items = items,
            Count = pagedRecipes.Count,
            Limit = pagedRecipes.Limit,
            Start = pagedRecipes.Start
        };

        return paginatedResponse;
    }
    
    public RecipeResponse? GetById(int id)
    {
        var recipe = _recipeRepository.GetById(id);
        if (recipe == null) return null;

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

        var recipe = _recipeRepository.GetByTitle(title);
        if (recipe == null) return null;

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
        var recipe = new Recipe
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
        var existingRecipe = _recipeRepository.GetById(request.Id);
        if (existingRecipe == null) return false;

        var recipeToUpdate = new Recipe
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