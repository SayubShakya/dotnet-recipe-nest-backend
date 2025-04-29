using System.Net;
using System.Text.RegularExpressions;
using RecipeNest.Constant;
using RecipeNest.Controller;
using RecipeNest.Dto;
using RecipeNest.Request;
using RecipeNest.Response;
using RecipeNest.Util.Impl;

namespace RecipeNest.Router;

public class RecipeRouter
{
    private readonly RecipeController _recipeController;
    private readonly SessionUser _sessionUser;

    public RecipeRouter(RecipeController recipeController, SessionUser sessionUser)
    {
        _recipeController = recipeController;
        _sessionUser = sessionUser;
    }

    public ServerResponse Recipe(string path, HttpListenerRequest request)
    {
        Console.WriteLine("Recipe path check: " + path);

        if (Regex.IsMatch(path, @"^/recipes\?id=\d+$"))
        {
            int id = int.Parse(request.QueryString["id"]!);

            if (request.HttpMethod.Equals("GET")) return _recipeController.GetById(id);

            if (request.HttpMethod.Equals("DELETE")) return _recipeController.DeleteById(id);

            return ResponseUtil.Unauthorized();
        }

        if (Regex.IsMatch(path, @"^/recipes/?(?:\?.*)?$"))
        {
            int start = int.Parse(request.QueryString["start"] ?? IApplicationConstant.DefaultStart);
            int limit = int.Parse(request.QueryString["limit"] ?? IApplicationConstant.DefaultLimit);

            if (request.HttpMethod.Equals("GET")) return _recipeController.GetAll(start, limit);

            if (_sessionUser.Authenticated)
            {
                if (request.HttpMethod.Equals("POST"))
                    return _recipeController.Save(BaseController.JsonRequestBody<CreateRecipeRequest>(request));

                if (request.HttpMethod.Equals("PUT"))
                    return _recipeController.Update(BaseController.JsonRequestBody<UpdateRecipeRequest>(request));
            }
            else
            {
                return ResponseUtil.Unauthorized();
            }
        }

        return ResponseUtil.NotFound();
    }
}