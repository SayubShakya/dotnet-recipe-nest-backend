//APIRouter.cs

using System.Net;
using System.Text.RegularExpressions;
using RecipeNest.Controller;
using RecipeNest.Reponse;
using RecipeNest.Request;

namespace RecipeNest.Router;

public class APIRouter
{
    private readonly CuisineController cuisineController;
    private readonly FavoriteController favoriteController;
    private readonly RatingController ratingController;
    private readonly RecipeController recipeController;
    private readonly RoleController roleController;
    private readonly UserController userController;

    private static readonly string defaultLimit = "10";
    private static readonly string defaultStart = "1";
    

    public APIRouter(RoleController roleController, UserController userController,
        CuisineController cuisineController, RecipeController recipeController,
        FavoriteController favoriteController, RatingController ratingController)
    {
        this.roleController = roleController;
        this.userController = userController;
        this.cuisineController = cuisineController;
        this.recipeController = recipeController;
        this.favoriteController = favoriteController;
        this.ratingController = ratingController;
    }

    public string Route(HttpListenerRequest request)
    {
        try
        {
            var path = request.Url.AbsolutePath + request.Url.Query;
            Console.WriteLine("requesting path: " + path);

            if (path.StartsWith("/api/rest/"))
            {
                path = path.Replace("/api/rest", "");

                if (path.Contains("/roles")) return Role(path, request);

                if (path.Contains("/users")) return User(path, request);

                if (path.Contains("/cuisines")) return Cuisine(path, request);

                if (path.Contains("/recipes")) return Recipe(path, request);

                if (path.Contains("/favorites")) return Favorite(path, request);

                if (path.Contains("/ratings")) return Rating(path, request);
            }

            return NotFound();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BaseController.ToJsonResponse(new ServerResponse(null, "Internal Server Error", 500, e.Message));
        }
    }

    public string Role(string path, HttpListenerRequest request)
    {
        Console.WriteLine("requesting path: " + path);

        if (Regex.IsMatch(path, @"^/roles/?$"))
        {
            if (request.HttpMethod.Equals("GET")) return roleController.GetAll();

            if (request.HttpMethod.Equals("POST"))
                return roleController.Save(BaseController.JsonRequestBody<CreateRoleRequest>(request));

            if (request.HttpMethod.Equals("PUT"))
                return roleController.Update(BaseController.JsonRequestBody<UpdateRoleRequest>(request));
        }
        else if (Regex.IsMatch(path, @"^/roles\?id=\d+$"))
        {
            var id = Convert.ToInt32(request.QueryString["id"]);

            if (request.HttpMethod.Equals("GET")) return roleController.GetById(id);

            if (request.HttpMethod.Equals("DELETE")) return roleController.DeleteById(id);
        }

        return NotFound();
    }

    public string User(string path, HttpListenerRequest request)
    {
        Console.WriteLine("requesting path: " + path);

        if (Regex.IsMatch(path, @"^/users/?$"))
        {
            if (request.HttpMethod.Equals("GET")) return userController.GetAll();

            if (request.HttpMethod.Equals("POST"))
                return userController.Save(BaseController.JsonRequestBody<CreateUserRequest>(request));

            if (request.HttpMethod.Equals("PUT"))
                return userController.Update(BaseController.JsonRequestBody<UpdateUserRequest>(request));
        }
        else if (Regex.IsMatch(path, @"^/users\?id=\d+$"))
        {
            var id = Convert.ToInt32(request.QueryString["id"]);

            if (request.HttpMethod.Equals("GET")) return userController.GetById(id);

            if (request.HttpMethod.Equals("DELETE")) return userController.DeleteById(id);
        }
        else if (Regex.IsMatch(path, @"^/users\?email=[^&]+$"))
        {
            var email = request.QueryString["email"];
            if (request.HttpMethod.Equals("GET")) return userController.GetByEmail(email);
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
            
            if (request.HttpMethod.Equals("GET")) return cuisineController.GetAll(start, limit);

            if (request.HttpMethod.Equals("POST"))
                return cuisineController.Save(BaseController.JsonRequestBody<CreateCuisineRequest>(request));

            if (request.HttpMethod.Equals("PUT"))
                return cuisineController.Update(BaseController.JsonRequestBody<UpdateCuisineRequest>(request));
        }
        else if (Regex.IsMatch(path, @"^/cuisines\?id=\d+$"))
        {
            int id = int.Parse(request.QueryString["id"]!);

            if (request.HttpMethod.Equals("GET")) return cuisineController.GetById(id);

            if (request.HttpMethod.Equals("DELETE")) return cuisineController.DeleteById(id);

            return NotFound();
        }
        else if (Regex.IsMatch(path, @"^/cuisines\?name=.+$"))
        {
            var name = request.QueryString["name"];
            if (name != null && request.HttpMethod.Equals("GET"))
            {
                Console.WriteLine("Attempting to get cuisine by name: " + name);
                return cuisineController.GetByName(name);
            }
        }

        return NotFound();
    }


    public string Recipe(string path, HttpListenerRequest request)
    {
        Console.WriteLine("Recipe path check: " + path);

        if (Regex.IsMatch(path, @"^/recipes/?$"))
        {
            if (request.HttpMethod.Equals("GET")) return recipeController.GetAll();

            if (request.HttpMethod.Equals("POST"))
                return recipeController.Save(BaseController.JsonRequestBody<CreateRecipeRequest>(request));

            if (request.HttpMethod.Equals("PUT"))
                return recipeController.Update(BaseController.JsonRequestBody<UpdateRecipeRequest>(request));
        }
        else if (Regex.IsMatch(path, @"^/recipes\?id=\d+$"))
        {
            if (request.QueryString["id"] != null && int.TryParse(request.QueryString["id"], out var id))
            {
                if (request.HttpMethod.Equals("GET")) return recipeController.GetById(id);

                if (request.HttpMethod.Equals("DELETE")) return recipeController.DeleteById(id);
            }
            else
            {
                Console.WriteLine("Failed to parse ID from query string: " + request.QueryString["id"]);
            }
        }
        else if (Regex.IsMatch(path, @"^/recipes\?title=.+$"))
        {
            var title = request.QueryString["title"];
            if (title != null && request.HttpMethod.Equals("GET"))
            {
                Console.WriteLine("Attempting to get recipe by name: " + title);
                return recipeController.GetByTitle(title);
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
                return favoriteController.Save(BaseController.JsonRequestBody<CreateFavoriteRequest>(request));
        }
        else if (Regex.IsMatch(path, @"^/favorites\?user_id=\d+&recipe_id=\d+$"))
        {
            var userId = Convert.ToInt32(request.QueryString["user_id"]);
            var recipeId = Convert.ToInt32(request.QueryString["recipe_id"]);

            if (request.HttpMethod.Equals("GET")) return favoriteController.GetByUserAndRecipe(userId, recipeId);

            if (request.HttpMethod.Equals("DELETE")) return favoriteController.DeleteByUserAndRecipe(userId, recipeId);
        }

        return NotFound();
    }


    public string Rating(string path, HttpListenerRequest request)
    {
        Console.WriteLine("requesting Rating path: " + path);

        if (Regex.IsMatch(path, @"^/ratings/?$"))
        {
            if (request.HttpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase))
                return ratingController.Save(BaseController.JsonRequestBody<CreateRatingRequest>(request));
        }
        else if (Regex.IsMatch(path, @"^/ratings\?user_id=\d+&recipe_id=\d+$"))
        {
            if (request.QueryString["user_id"] != null
                && int.TryParse(request.QueryString["user_id"], out var userId)
                && request.QueryString["recipe_id"] != null
                && int.TryParse(request.QueryString["recipe_id"], out var recipeId))
            {
                if (request.HttpMethod.Equals("GET", StringComparison.OrdinalIgnoreCase))
                    return ratingController.GetByUserAndRecipe(userId, recipeId);

                if (request.HttpMethod.Equals("DELETE", StringComparison.OrdinalIgnoreCase))
                    return ratingController.DeleteByUserAndRecipe(userId, recipeId);
            }
            else
            {
                Console.WriteLine("Failed to parse user_id or recipe_id from rating query string.");
            }
        }

        return NotFound();
    }

    public string NotFound()
    {
        Console.WriteLine("Returning 404 Not Found");
        return BaseController.ToJsonResponse(new ServerResponse(null, "404 Not Found", 404));
    }
}