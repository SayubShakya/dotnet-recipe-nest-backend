using RecipeNest.CustomException;
using RecipeNest.Dto;
using RecipeNest.Entity;
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
    
    public 

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