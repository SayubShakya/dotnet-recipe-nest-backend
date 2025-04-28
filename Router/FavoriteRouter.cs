using System.Net;
using System.Text.RegularExpressions;
using RecipeNest.Constant;
using RecipeNest.Controller;
using RecipeNest.Dto;
using RecipeNest.Request;
using RecipeNest.Response;
using RecipeNest.Util.Impl;

namespace RecipeNest.Router;

public class FavoriteRouter
{
    private readonly FavoriteController _favoriteController;
    private readonly SessionUser _sessionUser;

    public FavoriteRouter(FavoriteController favoriteController, SessionUser sessionUser)
    {
        _favoriteController = favoriteController;
        _sessionUser = sessionUser;
    }

    public ServerResponse Favorite(string path, HttpListenerRequest request)
    {
        if (_sessionUser.Authenticated)
        {
            Console.WriteLine("requesting Favorite path: " + path);

            if (Regex.IsMatch(path, @"^/favorites/?(?:\?.*)?"))
            {
                if (request.HttpMethod.Equals("POST"))
                    return _favoriteController.Save(BaseController.JsonRequestBody<CreateFavoriteRequest>(request));

                if (request.HttpMethod.Equals("GET"))
                {
                    int start = int.Parse(request.QueryString["start"] ?? IApplicationConstant.DefaultStart);
                    int limit = int.Parse(request.QueryString["limit"] ?? IApplicationConstant.DefaultLimit);
                    return _favoriteController.GetAllFavorites(start, limit);
                }
            }

            return ResponseUtil.NotFound();
        }

        return ResponseUtil.Unauthorized();
    }
}