using RecipeNest.Response;
using RecipeNest.Request;
using RecipeNest.Service;

namespace RecipeNest.Controller;

public class FavoriteController : BaseController
{
    private readonly FavoriteService _favoriteService;

    public FavoriteController(FavoriteService favoriteService)
    {
        _favoriteService = favoriteService;
    }

    public ServerResponse GetAllFavorites(int start, int limit)
    {
        try
        {
            PaginatedResponse<RecipeResponse> response = _favoriteService.GetAll(start, limit);
            return new ServerResponse(response, null, 200);
        }
        catch (Exception ex)
        {
            return new ServerResponse(null, "Failed to retrieve favorite recipes.", 500, ex.Message);
        }
    }

    public ServerResponse Save(CreateFavoriteRequest request)
    {
        var success = _favoriteService.Save(request);
        if (success) return new ServerResponse(null, null, 201);
        return new ServerResponse(null, "Failed to create favorite", 400);
    }
    
}