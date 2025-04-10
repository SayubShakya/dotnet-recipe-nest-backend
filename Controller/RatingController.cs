using RecipeNest.Response;
using RecipeNest.Request;
using RecipeNest.Service;

namespace RecipeNest.Controller;

public class RatingController : BaseController
{
    private readonly RatingService _ratingService;

    public RatingController(RatingService ratingService)
    {
        _ratingService = ratingService;
    }

    public ServerResponse GetByUserAndRecipe(int userId, int recipeId)
    {
        try
        {
            var response = _ratingService.GetByUserAndRecipe(userId, recipeId);
            if (response == null) return new ServerResponse(null, "Rating not found", 404);
            return new ServerResponse(response, "Rating found", 200);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetByUserAndRecipe (RatingController): {ex.Message}");
            return new ServerResponse(null, "An internal error occurred", 500, ex.Message);
        }
    }

    public ServerResponse Save(CreateRatingRequest request)
    {
        if (request == null) return new ServerResponse(null, "Invalid rating request data", 400);
        try
        {
            var success = _ratingService.Save(request);
            if (success) return new ServerResponse(null, "Rating created successfully", 201);
            return new ServerResponse(null, "Failed to create rating", 400);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in Save (RatingController): {ex.Message}");
            return new ServerResponse(null, "An internal error occurred", 500, ex.Message);
        }
    }

    public ServerResponse DeleteByUserAndRecipe(int userId, int recipeId)
    {
        try
        {
            var success = _ratingService.DeleteByUserAndRecipe(userId, recipeId);
            if (success) return new ServerResponse(null, "Rating deleted successfully", 200);

            return new ServerResponse(null, "Rating not found or could not be deleted", 404);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in DeleteByUserAndRecipe (RatingController): {ex.Message}");
            return new ServerResponse(null, "An internal error occurred", 500, ex.Message);
        }
    }
}