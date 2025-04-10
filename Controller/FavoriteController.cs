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

    public string GetByUserAndRecipe(int userId, int recipeId)
    {
        var response = favoriteService.GetByUserAndRecipe(userId, recipeId);
        if (response == null) return ToJsonResponse(new ServerResponse(null, "Favorite not found", 404));

        return ToJsonResponse(new ServerResponse(response, "Favorite found", 200));
    }

    public string Save(CreateFavoriteRequest request)
    {
        if (request == null) return ToJsonResponse(new ServerResponse(null, "Invalid request", 400));

        var success = favoriteService.Save(request);

        if (success) return ToJsonResponse(new ServerResponse(null, "Favorite created successfully", 201));

        return ToJsonResponse(new ServerResponse(null,
            "Failed to create favorite - recipe/user not found or favorite already exists", 400));
    }

    public string DeleteByUserAndRecipe(int userId, int recipeId)
    {
        var success = favoriteService.DeleteByUserAndRecipe(userId, recipeId);
        if (success) return ToJsonResponse(new ServerResponse(null, "Favorite deleted", 200));

        return ToJsonResponse(new ServerResponse(null, "Favorite not found", 404));
    }
}