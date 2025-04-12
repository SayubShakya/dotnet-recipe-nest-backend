//APIRouter.cs

using System.Net;
using System.Text.RegularExpressions;
using Autofac;
using RecipeNest.Controller;
using RecipeNest.Dto;
using RecipeNest.Response;
using RecipeNest.Request;
using RecipeNest.Util;
using RecipeNest.Util.Impl;

namespace RecipeNest.Router;

public class APIRouter
{
    private readonly RoleRouter _roleRouter;
    private readonly UserRouter _userRouter;
    private readonly CuisineRouter _cuisineRouter;
    private readonly RecipeRouter _recipeRouter; 
    private readonly FavoriteRouter _favoriteRouter;
    private readonly RatingRouter _ratingRouter;
    private readonly AuthRouter _authRouter;

    public APIRouter(RoleRouter roleRouter,
        UserRouter userRouter,
        CuisineRouter cuisineRouter,
        RecipeRouter recipeRouter,
        FavoriteRouter favoriteRouter,
        RatingRouter ratingRouter,
        AuthRouter authRouter
    )
    {
        _roleRouter = roleRouter;
        _userRouter = userRouter; 
        _cuisineRouter = cuisineRouter;
        _recipeRouter = recipeRouter;
        _favoriteRouter = favoriteRouter;
        _ratingRouter = ratingRouter;
        _authRouter = authRouter;
    }

    private static readonly string defaultLimit = "10";
    private static readonly string defaultStart = "1";

    public ServerResponse Route(HttpListenerRequest request)
    {
        try
        {
            var path = request.Url.AbsolutePath + request.Url.Query;
            Console.WriteLine("requesting path: " + path);

            if (path.StartsWith("/api/rest/"))
            {
                path = path.Replace("/api/rest", "");

                if (path.Contains("/auth")) return _authRouter.Auth(path, request);
                
                if (true)
                {
                    if (path.Contains("/roles")) return _roleRouter.Role(path, request);

                    if (path.Contains("/users")) return _userRouter.User(path, request);

                    if (path.Contains("/cuisines")) return _cuisineRouter.Cuisine(path, request);

                    if (path.Contains("/recipes")) return _recipeRouter.Recipe(path, request);

                    if (path.Contains("/favorites")) return _favoriteRouter.Favorite(path, request);

                    if (path.Contains("/ratings")) return _ratingRouter.Rating(path, request);
                }
            }

            return ResponseUtil.NotFound();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new ServerResponse(null, "Internal Server Error", 500, e.Message);
        }
    }
    
}