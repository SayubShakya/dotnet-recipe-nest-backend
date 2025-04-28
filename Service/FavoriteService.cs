using RecipeNest.CustomException;
using RecipeNest.Dto;
using RecipeNest.Entity;
using RecipeNest.Projection;
using RecipeNest.Response;
using RecipeNest.Repository;
using RecipeNest.Request;

namespace RecipeNest.Service;

public class FavoriteService
{
    private readonly IFavoriteRepository _favoriteRepository;
    private readonly IRecipeRepository _recipeRepository;
    private readonly IUserRepository _userRepository;
    private readonly SessionUser _sessionUser;

    public FavoriteService(IFavoriteRepository favoriteRepository, IRecipeRepository recipeRepository, IUserRepository userRepository, SessionUser sessionUser)
    {
        _favoriteRepository = favoriteRepository;
        _recipeRepository = recipeRepository;
        _userRepository = userRepository;
        _sessionUser = sessionUser;
    }
    
    public PaginatedResponse<RecipeResponse> GetAll(int start, int limit)
    {
        Paged<RecipeProjection> pagedRecipes =
            _favoriteRepository.GetAllAuthorizedPaginated(start, limit, _sessionUser.User.Id);

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

    public bool Save(CreateFavoriteRequest request)
    {
        var userId = _sessionUser.User.Id;
        var existingFavorite = _favoriteRepository.GetByUserAndRecipe(userId, request.RecipeId);

        if (existingFavorite != null)
        {
            _favoriteRepository.DeleteById(existingFavorite.Id);
        }

        if (request.IsFavourite)
        {
            var favorite = new Favorite
            {
                UserId = userId,
                RecipeId = request.RecipeId
            };

            var success = _favoriteRepository.Save(favorite);
            return success;
        }
        

        return true;
    }
}