// RoleController.cs

using RecipeNest.Reponse;
using RecipeNest.Request;
using RecipeNest.Service;

namespace RecipeNest.Controller;

public class RoleController : BaseController
{
    private readonly RoleService roleService;

    public RoleController(RoleService roleService)
    {
        this.roleService = roleService;
    }

    public string GetAll()
    {
        var serverRespose = new ServerResponse(roleService.GetAll(), "Role list!", 200);
        return ToJsonResponse(serverRespose);
    }

    public string GetById(int id)
    {
        var serverRespose = new ServerResponse(roleService.GetById(id), "Role found!", 200);
        return ToJsonResponse(serverRespose);
    }

    public string Save(CreateRoleRequest request)
    {
        var success = roleService.Save(request);
        if (success) return ToJsonResponse(new ServerResponse(null, "Role has been created!", 201));

        return ToJsonResponse(new ServerResponse(null, "Role creation failed!", 400));
    }

    public string Update(UpdateRoleRequest request)
    {
        var success = roleService.Update(request);
        if (success) return ToJsonResponse(new ServerResponse(null, "Role has been updated!", 200));

        return ToJsonResponse(new ServerResponse(null, "Role has update failed!", 400));
    }

    public string DeleteById(int id)
    {
        var serverRespose = new ServerResponse(roleService.DeleteById(id), "Role deleted!", 200);
        return ToJsonResponse(serverRespose);
    }
}