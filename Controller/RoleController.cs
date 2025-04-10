// RoleController.cs

using RecipeNest.Request;
using RecipeNest.Response;
using RecipeNest.Service;

namespace RecipeNest.Controller;

public class RoleController : BaseController
{
    private readonly RoleService _roleService;

    public RoleController(RoleService roleService)
    {
        _roleService = roleService;
    }
    
    public string GetAll(int start, int limit)
    {
        try
        {
            PaginatedResponse<RoleResponse> response = _roleService.GetAll(start, limit);
            ServerResponse serverResponse = new ServerResponse(response, null, 200);
            return ToJsonResponse(serverResponse);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in RoleController.GetAll: {ex}");
            return ToJsonResponse(new ServerResponse(null, "Failed to retrieve roles.", 500, ex.Message));
        }
    }

    public string GetById(int id)
    {
        var serverRespose = new ServerResponse(_roleService.GetById(id), "Role found!", 200);
        return ToJsonResponse(serverRespose);
    }

    public string Save(CreateRoleRequest request)
    {
        var success = _roleService.Save(request);
        if (success) return ToJsonResponse(new ServerResponse(null, "Role has been created!", 201));

        return ToJsonResponse(new ServerResponse(null, "Role creation failed!", 400));
    }

    public string Update(UpdateRoleRequest request)
    {
        var success = _roleService.Update(request);
        if (success) return ToJsonResponse(new ServerResponse(null, "Role has been updated!", 200));

        return ToJsonResponse(new ServerResponse(null, "Role has update failed!", 400));
    }

    public string DeleteById(int id)
    {
        var serverRespose = new ServerResponse(_roleService.DeleteById(id), "Role deleted!", 200);
        return ToJsonResponse(serverRespose);
    }
}