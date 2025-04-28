using System.Net;
using System.Text.RegularExpressions;
using RecipeNest.Constant;
using RecipeNest.Controller;
using RecipeNest.Dto;
using RecipeNest.Request;
using RecipeNest.Response;
using RecipeNest.Util.Impl;

namespace RecipeNest.Router;

public class UserRouter
{
    private readonly UserController _userController;
    private readonly SessionUser _sessionUser;


    public UserRouter(UserController userController, SessionUser sessionUser)
    {
        _userController = userController;
        _sessionUser = sessionUser;
    }

    public ServerResponse User(string path, HttpListenerRequest request)
    {
        if (_sessionUser.Authenticated)
        {
            Console.WriteLine("User requesting path: " + path);
            if (Regex.IsMatch(path, @"^/users\?id=\d+$"))
            {
                int id = int.Parse(request.QueryString["id"]!);

                if (request.HttpMethod.Equals("GET")) return _userController.GetById(id);
            }
            else if (Regex.IsMatch(path, @"^/users/profile/?$") && request.HttpMethod.Equals("PUT"))
            {
                return _userController.UpdateProfile(BaseController.JsonRequestBody<UpdateUserProfileRequest>(request));
            }

            else if (Regex.IsMatch(path, @"^/users/chefs/?(?:\?.*)?$") && request.HttpMethod.Equals("GET"))
            {
                int start = int.Parse(request.QueryString["start"] ?? IApplicationConstant.DefaultStart);
                int limit = int.Parse(request.QueryString["limit"] ?? IApplicationConstant.DefaultLimit);
                return _userController.GetAllChef(start, limit);
            }
            else if (Regex.IsMatch(path, @"^/users/status-toggle/?$"))
            {
                if (request.HttpMethod.Equals("POST"))
                {
                    if (request.HttpMethod.Equals("POST"))
                        return _userController.ToggleUserActivation(
                            BaseController.JsonRequestBody<ToggleUserStatusRequest>(request));
                }
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

        return ResponseUtil.Unauthorized();
    }
}