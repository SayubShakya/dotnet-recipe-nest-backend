using System.Net;
using System.Text.RegularExpressions;
using RecipeNest.Constant;
using RecipeNest.Controller;
using RecipeNest.Request;
using RecipeNest.Response;
using RecipeNest.Util.Impl;

namespace RecipeNest.Router;

public class CuisineRouter
{
    
    private readonly CuisineController _cuisineController;

    public CuisineRouter(CuisineController cuisineController)
    {
        _cuisineController = cuisineController;
    }
    
    public ServerResponse Cuisine(string path, HttpListenerRequest request)
    {
        Console.WriteLine("Cuisine path check: " + path);
        if (Regex.IsMatch(path, @"^/cuisines\?id=\d+$"))
        {
            int id = int.Parse(request.QueryString["id"]!);

            if (request.HttpMethod.Equals("GET")) return _cuisineController.GetById(id);

            if (request.HttpMethod.Equals("DELETE")) return _cuisineController.DeleteById(id);
        }
        else if (Regex.IsMatch(path, @"^/cuisines\?name=.+$"))
        {
            var name = request.QueryString["name"];
            if (name != null && request.HttpMethod.Equals("GET"))
            {
                Console.WriteLine("Attempting to get cuisine by name: " + name);
                return _cuisineController.GetByName(name);
            }
        }
        else if (Regex.IsMatch(path, @"^/cuisines/?(?:\?.*)?"))
        {
            int start = int.Parse(request.QueryString["start"] ?? IApplicationConstant.DefaultStart);
            int limit = int.Parse(request.QueryString["limit"] ?? IApplicationConstant.DefaultLimit);

            if (request.HttpMethod.Equals("GET")) return _cuisineController.GetAll(start, limit);

            if (request.HttpMethod.Equals("POST"))
                return _cuisineController.Save(BaseController.JsonRequestBody<CreateCuisineRequest>(request));

            if (request.HttpMethod.Equals("PUT"))
                return _cuisineController.Update(BaseController.JsonRequestBody<UpdateCuisineRequest>(request));
        }
        return ResponseUtil.NotFound();
    }

}