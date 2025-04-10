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

        if (ratingModel == null) return null;

        if (ratingModel.UserId == null || ratingModel.RecipeId == null || ratingModel.Score == null)
        {
            Console.WriteLine(
                $"Warning: Fetched rating (ID: {ratingModel.Id}) has unexpected null values for required fields.");
            return null;
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
            Console.WriteLine("RatingService.Save: Request is null.");
            return false;
        }

        var user = _userRepository.GetById(request.UserId);
        if (user == null)
        {
            Console.WriteLine($"RatingService.Save: User not found for ID: {request.UserId}");
            return false;
        }

        var recipe = _recipeRepository.GetById(request.RecipeId);
        if (recipe == null)
        {
            Console.WriteLine($"RatingService.Save: Recipe not found for ID: {request.RecipeId}");
            return false;
        }

        var ratingModel = new Rating
        {
            UserId = request.UserId,
            RecipeId = request.RecipeId,
            Score = request.Score
        };

        var success = _ratingRepository.Save(ratingModel);
        if (!success)
            Console.WriteLine(
                $"RatingService.Save: Repository failed to save rating for UserID: {request.UserId}, RecipeID: {request.RecipeId}.");

        return success;
    }

    public bool DeleteByUserAndRecipe(int userId, int recipeId)
    {
        var success = _ratingRepository.DeleteByUserAndRecipe(userId, recipeId);
        if (!success)
            Console.WriteLine(
                $"RatingService.Delete: Repository failed to delete rating for UserID: {userId}, RecipeID: {recipeId}.");

        return success;
    }
}