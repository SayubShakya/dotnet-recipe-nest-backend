using System.Net;
using System.Text.RegularExpressions;
using RecipeNest.Constant;
using RecipeNest.Controller;
using RecipeNest.Request;
using RecipeNest.Response;
using RecipeNest.Util.Impl;

namespace RecipeNest.Router;

public class UserRouter
{
    
    private readonly UserController _userController;

    public UserRouter(UserController userController)
    {
        _userController = userController;
    }
    
    public ServerResponse User(string path, HttpListenerRequest request)
    {
        Console.WriteLine("User requesting path: " + path);
        if (Regex.IsMatch(path, @"^/users\?id=\d+$"))
        {
            int id = int.Parse(request.QueryString["id"]!);

            if (request.HttpMethod.Equals("GET")) return _userController.GetById(id);

            if (request.HttpMethod.Equals("DELETE")) return _userController.DeleteById(id);
        }
        else if (Regex.IsMatch(path, @"^/users/?(?:\?.*)?"))
        {
            int start = int.Parse(request.QueryString["start"] ?? IApplicationConstant.DefaultStart);
            int limit = int.Parse(request.QueryString["limit"] ?? IApplicationConstant.DefaultLimit);

            if (request.HttpMethod.Equals("GET")) return _userController.GetAll(start, limit);

            if (request.HttpMethod.Equals("POST"))
                return _userController.Save(BaseController.JsonRequestBody<CreateUserRequest>(request));

            if (request.HttpMethod.Equals("PUT"))
                return _userController.Update(BaseController.JsonRequestBody<UpdateUserRequest>(request));
        }
        return ResponseUtil.NotFound();
    }
}