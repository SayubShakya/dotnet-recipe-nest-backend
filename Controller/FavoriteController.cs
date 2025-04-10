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
    public ServerResponse GetByUserAndRecipe(int userId, int recipeId)
    {
        var response = favoriteService.GetByUserAndRecipe(userId, recipeId);
        if (response == null) return new ServerResponse(null, "Favorite not found", 404);
        return new ServerResponse(response, "Favorite found", 200);
    }

    public ServerResponse Save(CreateFavoriteRequest request)
    {
        if (request == null) return new ServerResponse(null, "Invalid request", 400);
        var success = favoriteService.Save(request);
        if (success) return new ServerResponse(null, "Favorite created successfully", 201);
        return new ServerResponse(null, "Failed to create favorite - recipe/user not found or favorite already exists", 400);
    }

    public ServerResponse DeleteByUserAndRecipe(int userId, int recipeId)
    {
        var success = favoriteService.DeleteByUserAndRecipe(userId, recipeId);
        if (success) return new ServerResponse(null, "Favorite deleted", 200);
        return new ServerResponse(null, "Favorite not found", 404);
    }
}