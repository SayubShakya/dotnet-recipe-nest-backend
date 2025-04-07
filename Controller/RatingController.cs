using RecipeNest.Reponse;
using RecipeNest.Request;
using RecipeNest.Service;
using System;

namespace RecipeNest.Controller
{
    public class RatingController : BaseController
    {
        private readonly RatingService _ratingService;

        public RatingController(RatingService ratingService)
        {
            this._ratingService = ratingService;
        }

        public string GetByUserAndRecipe(int userId, int recipeId)
        {
            try
            {
                var response = _ratingService.GetByUserAndRecipe(userId, recipeId);
                if (response == null)
                {
                    return ToJsonResponse(new ServerResponse(null, "Rating not found", 404));
                }

                return ToJsonResponse(new ServerResponse(response, "Rating found", 200));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetByUserAndRecipe (RatingController): {ex.Message}");
                return ToJsonResponse(new ServerResponse(null, "An internal error occurred", 500, ex.Message));
            }
        }

        public string Save(CreateRatingRequest request)
        {
            if (request == null)
            {
                return ToJsonResponse(new ServerResponse(null, "Invalid rating request data", 400));
            }

            try
            {
                bool success = _ratingService.Save(request);

                if (success)
                {
                    return ToJsonResponse(new ServerResponse(null, "Rating created successfully", 201));
                }
                else
                {
                    return ToJsonResponse(new ServerResponse(null, "Failed to create rating", 400));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Save (RatingController): {ex.Message}");
                return ToJsonResponse(new ServerResponse(null, "An internal error occurred", 500, ex.Message));
            }
        }

        public string DeleteByUserAndRecipe(int userId, int recipeId)
        {
            try
            {
                bool success = _ratingService.DeleteByUserAndRecipe(userId, recipeId);
                if (success)
                {
                    return ToJsonResponse(new ServerResponse(null, "Rating deleted successfully", 200));
                }
                else
                {
                    return ToJsonResponse(new ServerResponse(null, "Rating not found or could not be deleted", 404));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteByUserAndRecipe (RatingController): {ex.Message}");
                return ToJsonResponse(new ServerResponse(null, "An internal error occurred", 500, ex.Message));
            }
        }
    }
}