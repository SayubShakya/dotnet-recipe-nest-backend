//APIRouter.cs

using System.Net;
using System.Text.RegularExpressions;
using Autofac;
using RecipeNest.Controller;
using RecipeNest.Dto;
using RecipeNest.Response;
using RecipeNest.Request;
using RecipeNest.Util.Impl;

namespace RecipeNest.Router;

public class APIRouter
{
    private readonly RoleController _roleController;
    private readonly UserController _userController;
    private readonly CuisineController _cuisineController;
    private readonly RecipeController _recipeController;
    private readonly FavoriteController _favoriteController;
    private readonly RatingController _ratingController;
    private readonly AuthController _authController;
    private readonly ILifetimeScope _lifetimeScope;

    public APIRouter(RoleController roleController,
        UserController userController,
        CuisineController cuisineController,
        RecipeController recipeController,
        FavoriteController favoriteController,
        RatingController ratingController,
        AuthController authController,
        ILifetimeScope lifetimeScope)
    {
        _roleController = roleController;
        _userController = userController;
        _cuisineController = cuisineController;
        _recipeController = recipeController;
        _favoriteController = favoriteController;
        _ratingController = ratingController;
        _authController = authController;
        _lifetimeScope = lifetimeScope;
    }
    
    private static readonly string defaultLimit = "10";
    private static readonly string defaultStart = "1";
    
    public string Route(HttpListenerRequest request)
    {
        try
        {
            var path = request.Url.AbsolutePath + request.Url.Query;
            Console.WriteLine("requesting path: " + path);

            if (path.StartsWith("/api/rest/"))
            {
                path = path.Replace("/api/rest", "");
                
                if (path.Contains("/auth")) return Auth(path, request);
                
                var sessionUserDto = _lifetimeScope.Resolve<SessionUserDTO>();
                
                if (sessionUserDto.Authenticated)
                {
                    
                    if (path.Contains("/roles")) return Role(path, request);

                    if (path.Contains("/users")) return User(path, request);

                    if (path.Contains("/cuisines")) return Cuisine(path, request);

                    if (path.Contains("/recipes")) return Recipe(path, request);

                    if (path.Contains("/favorites")) return Favorite(path, request);

                    if (path.Contains("/ratings")) return Rating(path, request);
                }
                else
                {
                    return Unauthorized();
                }
                
            }

            return NotFound();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BaseController.ToJsonResponse(new ServerResponse(null, "Internal Server Error", 500, e.Message));
        }
    }

    public string Auth(string path, HttpListenerRequest request)
    {
        Console.WriteLine("Role requesting path: " + path);

        if (Regex.IsMatch(path, @"^/auth/login(?:&.*)?$"))
        {
            if (request.HttpMethod.Equals("POST")) return _authController.Login(BaseController.JsonRequestBody<LoginRequest>(request));
        }

        return NotFound();
    }
    
    public string Role(string path, HttpListenerRequest request)
    {
        Console.WriteLine("Role requesting path: " + path);

        if (Regex.IsMatch(path, @"^/roles/?(?:\?.*)?"))
        {
            int start = int.Parse(request.QueryString["start"] ?? defaultStart);
            int limit = int.Parse(request.QueryString["limit"] ?? defaultLimit);
            
            if (request.HttpMethod.Equals("GET")) return _roleController.GetAll(start, limit);

            if (request.HttpMethod.Equals("POST"))
                return _roleController.Save(BaseController.JsonRequestBody<CreateRoleRequest>(request));

            if (request.HttpMethod.Equals("PUT"))
                return _roleController.Update(BaseController.JsonRequestBody<UpdateRoleRequest>(request));
        }
        else if (Regex.IsMatch(path, @"^/roles\?id=\d+$"))
        {
            int id = int.Parse(request.QueryString["id"]!);

            if (request.HttpMethod.Equals("GET")) return _roleController.GetById(id);

            if (request.HttpMethod.Equals("DELETE")) return _roleController.DeleteById(id);
        }

        return NotFound();
    }

    public string User(string path, HttpListenerRequest request)
    {
        Console.WriteLine("User requesting path: " + path);

        if (Regex.IsMatch(path, @"^/users/?(?:\?.*)?"))
        {
            int start = int.Parse(request.QueryString["start"] ?? defaultStart);
            int limit = int.Parse(request.QueryString["limit"] ?? defaultLimit);
            
            if (request.HttpMethod.Equals("GET")) return _userController.GetAll(start, limit);

            if (request.HttpMethod.Equals("POST"))
                return _userController.Save(BaseController.JsonRequestBody<CreateUserRequest>(request));

            if (request.HttpMethod.Equals("PUT"))
                return _userController.Update(BaseController.JsonRequestBody<UpdateUserRequest>(request));
        }
        else if (Regex.IsMatch(path, @"^/users\?id=\d+$"))
        {
            int id = int.Parse(request.QueryString["id"]!);

            if (request.HttpMethod.Equals("GET")) return _userController.GetById(id);

            if (request.HttpMethod.Equals("DELETE")) return _userController.DeleteById(id);
        }
        else if (Regex.IsMatch(path, @"^/users\?email=[^&]+$"))
        {
            var email = request.QueryString["email"];
            if (request.HttpMethod.Equals("GET")) return _userController.GetByEmail(email);
        }

        return NotFound();
    }


    public string Cuisine(string path, HttpListenerRequest request)
    {
        Console.WriteLine("Cuisine path check: " + path);

        if (Regex.IsMatch(path, @"^/cuisines/?(?:\?.*)?"))
        {
            int start = int.Parse(request.QueryString["start"] ?? defaultStart);
            int limit = int.Parse(request.QueryString["limit"] ?? defaultLimit);
            
            if (request.HttpMethod.Equals("GET")) return _cuisineController.GetAll(start, limit);

            if (request.HttpMethod.Equals("POST"))
                return _cuisineController.Save(BaseController.JsonRequestBody<CreateCuisineRequest>(request));

            if (request.HttpMethod.Equals("PUT"))
                return _cuisineController.Update(BaseController.JsonRequestBody<UpdateCuisineRequest>(request));
        }
        else if (Regex.IsMatch(path, @"^/cuisines\?id=\d+$"))
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

        return NotFound();
    }


    public string Recipe(string path, HttpListenerRequest request)
    {
        Console.WriteLine("Recipe path check: " + path);

        if (Regex.IsMatch(path, @"^/recipes/?(?:\?.*)?$"))
        {
            int start = int.Parse(request.QueryString["start"] ?? defaultStart);
            int limit = int.Parse(request.QueryString["limit"] ?? defaultLimit);
        
            if (request.HttpMethod.Equals("GET")) return _recipeController.GetAll(start, limit);

            if (request.HttpMethod.Equals("POST"))
                return _recipeController.Save(BaseController.JsonRequestBody<CreateRecipeRequest>(request));

            if (request.HttpMethod.Equals("PUT"))
                return _recipeController.Update(BaseController.JsonRequestBody<UpdateRecipeRequest>(request));
        }
        else if (Regex.IsMatch(path, @"^/recipes\?id=\d+(?:&.*)?$"))
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

        return NotFound();
    }


    public string Favorite(string path, HttpListenerRequest request)
    {
        Console.WriteLine("requesting Favorite path: " + path);
        if (Regex.IsMatch(path, @"^/favorites/?$"))
        {
            if (request.HttpMethod.Equals("POST"))
                return _favoriteController.Save(BaseController.JsonRequestBody<CreateFavoriteRequest>(request));
        }
        else if (Regex.IsMatch(path, @"^/favorites\?user_id=\d+&recipe_id=\d+$"))
        {
            var userId = Convert.ToInt32(request.QueryString["user_id"]);
            var recipeId = Convert.ToInt32(request.QueryString["recipe_id"]);

            if (request.HttpMethod.Equals("GET")) return _favoriteController.GetByUserAndRecipe(userId, recipeId);
            if (request.HttpMethod.Equals("DELETE")) return _favoriteController.DeleteByUserAndRecipe(userId, recipeId);
        }

        return NotFound();
    }


    public string Rating(string path, HttpListenerRequest request)
    {
        Console.WriteLine("requesting Rating path: " + path);

        if (Regex.IsMatch(path, @"^/ratings/?$"))
        {
            if (request.HttpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase))
                return _ratingController.Save(BaseController.JsonRequestBody<CreateRatingRequest>(request));
        }
        else if (Regex.IsMatch(path, @"^/ratings\?user_id=\d+&recipe_id=\d+$"))
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

        return ResponseUtil.NotFound();
    }
}