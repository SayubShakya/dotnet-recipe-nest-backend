using System.Net;
using System.Text.RegularExpressions;
using RecipeNest.Constant;
using RecipeNest.Controller;
using RecipeNest.Request;
using RecipeNest.Response;
using RecipeNest.Util.Impl;

namespace RecipeNest.Router;

public class RecipeRouter
{
    private readonly RecipeController _recipeController;

    public RecipeRouter(RecipeController recipeController)
    {
        _recipeController = recipeController;
    }
    
    public ServerResponse Recipe(string path, HttpListenerRequest request)
    {
        Console.WriteLine("Recipe path check: " + path);
        
        if (Regex.IsMatch(path, @"^/recipes\?id=\d+(?:&.*)?$"))
        {
            int id = int.Parse(request.QueryString["id"]!);
            if (request.HttpMethod.Equals("GET")) return _recipeController.GetById(id);
            if (request.HttpMethod.Equals("DELETE")) return _recipeController.DeleteById(id);
        }
        
        else if (Regex.IsMatch(path, @"^/recipes\?title=[^&]+(?:&.*)?$"))
        {
            var title = request.QueryString["title"];
            if (title != null && request.HttpMethod.Equals("GET"))
            {
                Console.WriteLine("Attempting to get recipe by name: " + title);
                return _recipeController.GetByTitle(title);
            }
        }
        else if (Regex.IsMatch(path, @"^/recipes/favorites\?user_id=\d+(?:&.*)?$") && request.HttpMethod.Equals("GET"))
        {
            int userId = int.Parse(request.QueryString["user_id"]!);
            int start = int.Parse(request.QueryString["start"] ?? IApplicationConstant.DefaultStart);
            int limit = int.Parse(request.QueryString["limit"] ?? IApplicationConstant.DefaultLimit);
            
            return _recipeController.GetAllFavorites(userId, start, limit);
        }
        
        else if (Regex.IsMatch(path, @"^/recipes/?(?:\?.*)?$"))
        {
            int start = int.Parse(request.QueryString["start"] ?? IApplicationConstant.DefaultStart);
            int limit = int.Parse(request.QueryString["limit"] ?? IApplicationConstant.DefaultLimit);

            if (request.HttpMethod.Equals("GET")) return _recipeController.GetAll(start, limit);

            if (request.HttpMethod.Equals("POST"))
                return _recipeController.Save(BaseController.JsonRequestBody<CreateRecipeRequest>(request));

            if (request.HttpMethod.Equals("PUT"))
                return _recipeController.Update(BaseController.JsonRequestBody<UpdateRecipeRequest>(request));
        }
        return ResponseUtil.NotFound();
    }
}