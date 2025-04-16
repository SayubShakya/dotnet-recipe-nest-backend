using System.Net;
using System.Text.RegularExpressions;
using RecipeNest.Constant;
using RecipeNest.Controller;
using RecipeNest.Request;
using RecipeNest.Response;
using RecipeNest.Util.Impl;

namespace RecipeNest.Router;

public class RoleRouter
{
    private readonly RoleController _roleController;

    public RoleRouter(RoleController roleController)
    {
        _roleController = roleController;
    }

    public ServerResponse Role(string path, HttpListenerRequest request)
    {
        Console.WriteLine("Role requesting path: " + path);
        if (Regex.IsMatch(path, @"^/roles\?id=\d+$"))
        {
            int id = int.Parse(request.QueryString["id"]!);

            if (request.HttpMethod.Equals("GET")) return _roleController.GetById(id);

            if (request.HttpMethod.Equals("DELETE")) return _roleController.DeleteById(id);
        }

        else if (Regex.IsMatch(path, @"^/roles/?(?:\?.*)?"))
        {
            int start = int.Parse(request.QueryString["start"] ?? IApplicationConstant.DefaultStart);
            int limit = int.Parse(request.QueryString["limit"] ?? IApplicationConstant.DefaultLimit);
            
            if (request.HttpMethod.Equals("GET")) return _roleController.GetAll(start, limit);

            if (request.HttpMethod.Equals("POST"))
                return _roleController.Save(BaseController.JsonRequestBody<CreateRoleRequest>(request));

            if (request.HttpMethod.Equals("PUT"))
                return _roleController.Update(BaseController.JsonRequestBody<UpdateRoleRequest>(request));
        }
        return ResponseUtil.NotFound();
    }
}