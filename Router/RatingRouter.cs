using System.Net;
using System.Text.RegularExpressions;
using RecipeNest.Controller;
using RecipeNest.Dto;
using RecipeNest.Request;
using RecipeNest.Response;
using RecipeNest.Util.Impl;

namespace RecipeNest.Router;

public class RatingRouter
{
    
    
    private readonly RatingController _ratingController;
    private readonly SessionUser _sessionUser;


    public RatingRouter(RatingController ratingController, SessionUser sessionUser)
    {
        _ratingController = ratingController;
        _sessionUser = sessionUser;
    }

    public ServerResponse Rating(string path, HttpListenerRequest request)
    {
        
        if (_sessionUser.Authenticated)
        {
        Console.WriteLine("requesting Rating path: " + path);
        
        if (Regex.IsMatch(path, @"^/ratings\?user_id=\d+&recipe_id=\d+$"))
        {
            if (request.QueryString["user_id"] != null
                && int.TryParse(request.QueryString["user_id"], out var userId)
                && request.QueryString["recipe_id"] != null
                && int.TryParse(request.QueryString["recipe_id"], out var recipeId))
            {
                if (request.HttpMethod.Equals("GET", StringComparison.OrdinalIgnoreCase))
                    return _ratingController.GetByUserAndRecipe(userId, recipeId);

                if (request.HttpMethod.Equals("DELETE", StringComparison.OrdinalIgnoreCase))
                    return _ratingController.DeleteByUserAndRecipe(userId, recipeId);
            }
        }

        else if (Regex.IsMatch(path, @"^/ratings/?$"))
        {
            if (request.HttpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase))
                return _ratingController.Save(BaseController.JsonRequestBody<CreateRatingRequest>(request));
        }

        return ResponseUtil.NotFound();
        }

        return ResponseUtil.Unauthorized();
    }
}