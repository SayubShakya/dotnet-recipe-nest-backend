using RecipeNest.Response;
using RecipeNest.Request;
using RecipeNest.Service;

namespace RecipeNest.Controller;

public class FavoriteController : BaseController
{
    private readonly FavoriteService favoriteService;

    public FavoriteController(FavoriteService favoriteService)
    {
        this.favoriteService = favoriteService;
    }
    public ServerResponse GetAll(int start, int limit)
    {
        var response = favoriteService.GetByUserAndRecipe(userId, recipeId);
        if (response == null) return new ServerResponse(null, "Favorite not found", 404);
        return new ServerResponse(response, "Favorite found", 200);
    }

    public ServerResponse Save(CreateFavoriteRequest request)
    {
        var success = favoriteService.Save(request);
        if (success) return new ServerResponse(null, "Favorite created successfully", 201);
        return new ServerResponse(null, "Failed to create favorite", 400);
    }
    
}