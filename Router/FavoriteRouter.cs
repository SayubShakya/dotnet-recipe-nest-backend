using System.Net;
using System.Text.RegularExpressions;
using RecipeNest.Controller;
using RecipeNest.Request;
using RecipeNest.Response;
using RecipeNest.Util.Impl;

namespace RecipeNest.Router;

public class FavoriteRouter
{
    
    private readonly FavoriteController _favoriteController;

    public FavoriteRouter(FavoriteController favoriteController)
    {
        _favoriteController = favoriteController;
    }
    
    
    public ServerResponse Favorite(string path, HttpListenerRequest request)
    {
        Console.WriteLine("requesting Favorite path: " + path);
        
        if (Regex.IsMatch(path, @"^/favorites\?user_id=\d+&recipe_id=\d+$"))
        {
            var userId = Convert.ToInt32(request.QueryString["user_id"]);
            var recipeId = Convert.ToInt32(request.QueryString["recipe_id"]);

            if (request.HttpMethod.Equals("GET")) return _favoriteController.GetByUserAndRecipe(userId, recipeId);
            if (request.HttpMethod.Equals("DELETE")) return _favoriteController.DeleteByUserAndRecipe(userId, recipeId);
        }
        
        else if (Regex.IsMatch(path, @"^/favorites/?$"))
        {
            if (request.HttpMethod.Equals("POST"))
                return _favoriteController.Save(BaseController.JsonRequestBody<CreateFavoriteRequest>(request));
        }
        return ResponseUtil.NotFound();
    }
}