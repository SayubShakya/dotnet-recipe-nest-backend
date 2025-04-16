using RecipeNest.CustomException;
using RecipeNest.Model;
using RecipeNest.Response;
using RecipeNest.Repository;
using RecipeNest.Request;

namespace RecipeNest.Service;

public class RatingService
{
    private readonly IRatingRepository _ratingRepository;
    private readonly IRecipeRepository _recipeRepository;
    private readonly IUserRepository _userRepository;

    public RatingService(IRatingRepository ratingRepository, IUserRepository userRepository,
        IRecipeRepository recipeRepository)
    {
        _ratingRepository = ratingRepository;
        _userRepository = userRepository;
        _recipeRepository = recipeRepository;
    }

    public RatingResponse GetByUserAndRecipe(int userId, int recipeId)
    {
        var ratingModel = _ratingRepository.GetByUserAndRecipe(userId, recipeId);
        if (ratingModel == null) throw new CustomApplicationException(404, "Rating not found", null);

        if (ratingModel.UserId == null || ratingModel.RecipeId == null || ratingModel.Score == null)
        {
            throw new CustomApplicationException(500, "Invalid rating data", null);
        }

        return new RatingResponse(
            ratingModel.Id,
            ratingModel.UserId.Value,
            ratingModel.RecipeId.Value,
            ratingModel.Score.Value
        );
    }

    public bool Save(CreateRatingRequest request)
    {
        if (request == null)
        {
            throw new CustomApplicationException(400, "Rating request cannot be null", null);
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

        var existingRating = _ratingRepository.GetByUserAndRecipe(request.UserId, request.RecipeId);
        if (existingRating != null)
        {
            throw new CustomApplicationException(409, "Rating already exists for this user and recipe", null);
        }

        var ratingModel = new Rating
        {
            UserId = request.UserId,
            RecipeId = request.RecipeId,
            Score = request.Score
        };

        var success = _ratingRepository.Save(ratingModel);
        if (!success)
        {
            throw new CustomApplicationException(500, "Failed to save rating", null);
        }

        return success;
    }

    public bool DeleteByUserAndRecipe(int userId, int recipeId)
    {
        var existingRating = _ratingRepository.GetByUserAndRecipe(userId, recipeId);
        if (existingRating == null)
        {
            throw new CustomApplicationException(404, "Rating not found", null);
        }

        var success = _ratingRepository.DeleteByUserAndRecipe(userId, recipeId);
        if (!success)
        {
            throw new CustomApplicationException(500, "Failed to delete rating", null);
        }

        return success;
    }
}