// RoleController.cs

using RecipeNest.CustomException;
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

    public ServerResponse GetAll(int start, int limit)
    {
        PaginatedResponse<RoleResponse> response = _roleService.GetAll(start, limit);
        ServerResponse serverResponse = new ServerResponse(response, null, 200);
        return serverResponse;
    }

    public ServerResponse GetById(int id)
    {
        return new ServerResponse(_roleService.GetById(id), "Role found!", 200);
    }

    public ServerResponse Save(CreateRoleRequest request)
    {
        var success = _roleService.Save(request);
        if (success) return new ServerResponse(null, "Role has been created!", 201);
        throw new CustomApplicationException(500, "Failed to save role!", null);
    }

    public ServerResponse Update(UpdateRoleRequest request)
    {
        var success = _roleService.Update(request);
        if (success) return new ServerResponse(null, "Role has been updated!", 200);
        throw new CustomApplicationException(500, "Failed to update role!", null);
    }

    public ServerResponse DeleteById(int id)
    {
        return new ServerResponse(_roleService.DeleteById(id), "Role deleted!", 200);
    }
}