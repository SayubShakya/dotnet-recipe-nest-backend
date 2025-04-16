using RecipeNest.CustomException;
using RecipeNest.Model;
using RecipeNest.Response;
using RecipeNest.Repository;
using RecipeNest.Request;

namespace RecipeNest.Service;

public class FavoriteService
{
    private readonly IFavoriteRepository _favoriteRepository;
    private readonly IRecipeRepository _recipeRepository;
    private readonly IUserRepository _userRepository;

    public FavoriteService(IFavoriteRepository favoriteRepository, IUserRepository userRepository,
        IRecipeRepository recipeRepository)
    {
        _favoriteRepository = favoriteRepository;
        _userRepository = userRepository;
        _recipeRepository = recipeRepository;
    }

    public FavoriteResponse GetByUserAndRecipe(int userId, int recipeId)
    {
        var favorite = _favoriteRepository.GetByUserAndRecipe(userId, recipeId);
        if (favorite == null) throw new CustomApplicationException(404, "Favorite not found", null);

        return new FavoriteResponse(favorite.Id, favorite.UserId, favorite.RecipeId);
    }

    public bool Save(CreateFavoriteRequest request)
    {
        if (request == null)
        {
            throw new CustomApplicationException(400, "Favorite request cannot be null", null);
        }

        var user = _userRepository.GetById(request.UserId);
        if (user == null)
        {
            throw new CustomApplicationException(404, $"User not found for ID: {request.UserId}", null);
        }

        var recipe = _recipeRepository.GetById(request.RecipeId);
        if (recipe == null)
        {
            throw new CustomApplicationException(404, $"Recipe not found for ID: {request.RecipeId}", null);
        }

        var existingFavorite = _favoriteRepository.GetByUserAndRecipe(request.UserId, request.RecipeId);
        if (existingFavorite != null)
        {
            throw new CustomApplicationException(409, "This recipe is already marked as favorite by the user", null);
        }

        var favorite = new Favorite
        {
            UserId = request.UserId,
            RecipeId = request.RecipeId
        };

        var success = _favoriteRepository.Save(favorite);
        if (!success)
        {
            throw new CustomApplicationException(500, "Failed to save favorite due to server error", null);
        }

        return success;
    }

    public bool DeleteByUserAndRecipe(int userId, int recipeId)
    {
        var existingFavorite = _favoriteRepository.GetByUserAndRecipe(userId, recipeId);
        if (existingFavorite == null)
        {
            throw new CustomApplicationException(404, "Favorite not found", null);
        }

        var success = _favoriteRepository.DeleteByUserAndRecipe(userId, recipeId);
        if (!success)
        {
            throw new CustomApplicationException(500, "Failed to delete favorite due to server error", null);
        }

        return success;
    }
}