using System;
using RecipeNest.Model;
using RecipeNest.Reponse; 
using RecipeNest.Repository;
using RecipeNest.Request;

namespace RecipeNest.Service
{
    public class RatingService
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRecipeRepository _recipeRepository;

        public RatingService(IRatingRepository ratingRepository, IUserRepository userRepository, IRecipeRepository recipeRepository)
        {
            _ratingRepository = ratingRepository;
            _userRepository = userRepository;
            _recipeRepository = recipeRepository;
        }

        public RatingResponse GetByUserAndRecipe(int userId, int recipeId)
        {
            Rating ratingModel = _ratingRepository.GetByUserAndRecipe(userId, recipeId);

            if (ratingModel == null)
            {
                return null;
            }

            if (ratingModel.UserId == null || ratingModel.RecipeId == null || ratingModel.Score == null)
            {
                Console.WriteLine($"Warning: Fetched rating (ID: {ratingModel.Id}) has unexpected null values for required fields.");
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

            Rating ratingModel = new Rating
            {
                UserId = request.UserId,
                RecipeId = request.RecipeId,
                Score = request.Score,
            };

            bool success = _ratingRepository.Save(ratingModel);
            if (!success)
            {
                 Console.WriteLine($"RatingService.Save: Repository failed to save rating for UserID: {request.UserId}, RecipeID: {request.RecipeId}.");
            }
            return success;
        }

        public bool DeleteByUserAndRecipe(int userId, int recipeId)
        {
            bool success = _ratingRepository.DeleteByUserAndRecipe(userId, recipeId);
             if (!success)
            {
                 Console.WriteLine($"RatingService.Delete: Repository failed to delete rating for UserID: {userId}, RecipeID: {recipeId}.");
            }
            return success;
        }

        
        }
    }