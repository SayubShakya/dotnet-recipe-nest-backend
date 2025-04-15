using System.Net;
using System.Text.RegularExpressions;
using RecipeNest.Controller;
using RecipeNest.Request;
using RecipeNest.Response;
using RecipeNest.Util.Impl;

namespace RecipeNest.Router;

public class AuthRouter
{
    private readonly AuthController _authController;

    public AuthRouter(AuthController authController)
    {
        _authController = authController;
    }
    
    public ServerResponse Auth(string path, HttpListenerRequest request)
    {
        Console.WriteLine("Role requesting path: " + path);

        if (Regex.IsMatch(path, @"^/auth/login/?$"))
        {
            if (request.HttpMethod.Equals("POST"))
                return _authController.Login(BaseController.JsonRequestBody<LoginRequest>(request));
        }
        else if (Regex.IsMatch(path, @"^/auth/register/?$"))
        {
            if (request.HttpMethod.Equals("POST"))
                return _authController.Register(BaseController.JsonRequestBody<RegisterRequest>(request));
        }
        else if (Regex.IsMatch(path, @"^/auth/authorized/?$"))
        {
            if (request.HttpMethod.Equals("POST"))
                return _authController.Register(BaseController.JsonRequestBody<RegisterRequest>(request));
        }
        
        return ResponseUtil.NotFound();
    }
}