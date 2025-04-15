// Service/RecipeService.cs

using RecipeNest.Dto;
using RecipeNest.Model;
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
    private readonly SessionUserDTO _sessionUserDto;

    public RecipeService(IRecipeRepository recipeRepository, IFavoriteRepository favoriteRepository, IRatingRepository ratingRepository, SessionUserDTO sessionUserDto)
    {
        _recipeRepository = recipeRepository;
        _favoriteRepository = favoriteRepository;
        _ratingRepository = ratingRepository;
        _sessionUserDto = sessionUserDto;
    }


    public PaginatedResponse<RecipeResponse> GetAll(int start, int limit)
    {
        Paged<RecipeAuthorized> pagedRecipes = _recipeRepository.GetAllAuthorizedPaginated(start, limit,_sessionUserDto.User.Id);
        
        List<RecipeResponse> items =  pagedRecipes.Items.Select(recipe =>
            {
                return new RecipeResponse(
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
                );
            }
            
            
           
        ).ToList();

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
        Rating rating =  _ratingRepository.GetByUserAndRecipe(_sessionUserDto.User.Id, recipe.Id);
        Favorite favorite =  _favoriteRepository.GetByUserAndRecipe(_sessionUserDto.User.Id, recipe.Id);
        return new RecipeResponse(
            recipe.Id,
            recipe.ImageUrl,
            recipe.Title,
            recipe.Description,
            recipe.RecipeDetail,
            recipe.Ingredients,
            recipe.RecipeByUserId,
            recipe.CuisineId,
            favorite !=null,
            rating !=null? (int) rating.Score: 0
        );
    }

    public RecipeResponse? GetByTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title)) return null;

        var recipe = _recipeRepository.GetByTitle(title);
        if (recipe == null) return null;
        Rating rating =  _ratingRepository.GetByUserAndRecipe(_sessionUserDto.User.Id, recipe.Id);
        Favorite favorite =  _favoriteRepository.GetByUserAndRecipe(_sessionUserDto.User.Id, recipe.Id);
        return new RecipeResponse(
            recipe.Id,
            recipe.ImageUrl,
            recipe.Title,
            recipe.Description,
            recipe.RecipeDetail,
            recipe.Ingredients,
            recipe.RecipeByUserId,
            recipe.CuisineId,
            favorite !=null,
            rating !=null? (int) rating.Score: 0
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
    
    public PaginatedResponse<RecipeResponse> GetAllFavorites(int userId, int start, int limit)
    {
        Paged<Recipe> pagedFavorites = _recipeRepository.GetFavoriteRecipes(userId, start, limit);

        List<RecipeResponse> items = pagedFavorites.Items.Select(recipe =>
        {
            Rating rating =  _ratingRepository.GetByUserAndRecipe(_sessionUserDto.User.Id, recipe.Id);
            Favorite favorite =  _favoriteRepository.GetByUserAndRecipe(_sessionUserDto.User.Id, recipe.Id);
            return new RecipeResponse(
                recipe.Id,
                recipe.ImageUrl,
                recipe.Title,
                recipe.Description,
                recipe.RecipeDetail,
                recipe.Ingredients,
                recipe.RecipeByUserId,
                recipe.CuisineId,
                favorite !=null,
                rating !=null? (int) rating.Score: 0
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