using RecipeNest.CustomException;
using RecipeNest.Dto;
using RecipeNest.Entity;
using RecipeNest.Projection;
using RecipeNest.Repository;
using RecipeNest.Request;
using RecipeNest.Response;

namespace RecipeNest.Service;

public class RecipeService
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly IFavoriteRepository _favoriteRepository;
    private readonly IRatingRepository _ratingRepository;
    private readonly SessionUser _sessionUser;

    public RecipeService(IRecipeRepository recipeRepository, IFavoriteRepository favoriteRepository,
        IRatingRepository ratingRepository, SessionUser sessionUser)
    {
        _recipeRepository = recipeRepository;
        _favoriteRepository = favoriteRepository;
        _ratingRepository = ratingRepository;
        _sessionUser = sessionUser;
    }

    public PaginatedResponse<RecipeResponse> GetAll(int start, int limit)
    {
        Paged<RecipeProjection> pagedRecipes = null;
        if (_sessionUser.Authenticated)
        {
          pagedRecipes =
                _recipeRepository.GetAllAuthorizedPaginated(start, limit, _sessionUser.User.Id);
        }
        else
        {
            pagedRecipes =
                _recipeRepository.GetAllPaginated(start, limit);
        }
     

        List<RecipeResponse> items = pagedRecipes.Items.Select(recipe => new RecipeResponse(
            recipe.Id,
            recipe.ImageUrl,
            recipe.Title,
            recipe.Description,
            recipe.RecipeDetail,
            recipe.Ingredients,
            recipe.RecipeByUserId,
            recipe.CuisineId,
            recipe.IsFavorite,
            recipe.Rating
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

    public RecipeResponse GetById(int id)
    {
        var recipe = _recipeRepository.GetActiveById(id);
        if (recipe == null) throw new CustomApplicationException(404, "Recipe not found", null);

        Rating rating = _ratingRepository.GetByUserAndRecipe(_sessionUser.User.Id, recipe.Id);
        Favorite favorite = _favoriteRepository.GetByUserAndRecipe(_sessionUser.User.Id, recipe.Id);
        return new RecipeResponse(
            recipe.Id,
            recipe.ImageUrl,
            recipe.Title,
            recipe.Description,
            recipe.RecipeDetail,
            recipe.Ingredients,
            recipe.RecipeByUserId,
            recipe.CuisineId,
            favorite != null,
            rating != null ? (int)rating.Score : 0
        );
    }

    public RecipeResponse GetByTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new CustomApplicationException(400, "Title cannot be empty", null);

        var recipe = _recipeRepository.GetByTitle(title);
        if (recipe == null) throw new CustomApplicationException(404, "Recipe not found", null);

        Rating rating = _ratingRepository.GetByUserAndRecipe(_sessionUser.User.Id, recipe.Id);
        Favorite favorite = _favoriteRepository.GetByUserAndRecipe(_sessionUser.User.Id, recipe.Id);
        return new RecipeResponse(
            recipe.Id,
            recipe.ImageUrl,
            recipe.Title,
            recipe.Description,
            recipe.RecipeDetail,
            recipe.Ingredients,
            recipe.RecipeByUserId,
            recipe.CuisineId,
            favorite != null,
            rating != null ? (int)rating.Score : 0
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
            RecipeByUserId = _sessionUser.User.Id,
            CuisineId = request.CuisineId
        };

        return _recipeRepository.Save(recipe);
    }

    public bool Update(UpdateRecipeRequest request)
    {
        var existingRecipe = _recipeRepository.GetActiveById(request.Id);
        if (existingRecipe == null) throw new CustomApplicationException(404, "Recipe not found", null);

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
        var existingRecipe = _recipeRepository.GetActiveById(id);
        if (existingRecipe == null) throw new CustomApplicationException(404, "Recipe not found", null);
        return _recipeRepository.DeleteById(id);
    }

    public PaginatedResponse<RecipeResponse> GetAllFavorites(int userId, int start, int limit)
    {
        Paged<Recipe> pagedFavorites = _recipeRepository.GetFavoriteRecipes(userId, start, limit);

        if (pagedFavorites == null || pagedFavorites.Count == 0)
            throw new CustomApplicationException(404, "No favorite recipes found", null);

        List<RecipeResponse> items = pagedFavorites.Items.Select(recipe =>
        {
            Rating rating = _ratingRepository.GetByUserAndRecipe(_sessionUser.User.Id, recipe.Id);
            Favorite favorite = _favoriteRepository.GetByUserAndRecipe(_sessionUser.User.Id, recipe.Id);
            return new RecipeResponse(
                recipe.Id,
                recipe.ImageUrl,
                recipe.Title,
                recipe.Description,
                recipe.RecipeDetail,
                recipe.Ingredients,
                recipe.RecipeByUserId,
                recipe.CuisineId,
                favorite != null,
                rating != null ? (int)rating.Score : 0
            );
        }).ToList();

        PaginatedResponse<RecipeResponse> paginatedResponse = new()
        {
            Items = items,
            Count = pagedFavorites.Count,
            Limit = pagedFavorites.Limit,
            Start = pagedFavorites.Start
        };

        return paginatedResponse;
    }
}