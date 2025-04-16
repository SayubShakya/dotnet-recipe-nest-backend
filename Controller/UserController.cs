// UserController.cs

using RecipeNest.Request;
using RecipeNest.Response;
using RecipeNest.Service;

namespace RecipeNest.Controller;

public class UserController : BaseController
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    public ServerResponse GetAll(int start, int limit)
    {
        return new ServerResponse(_userService.GetAll(start, limit), null, 200);
    }

    public ServerResponse GetById(int id)
    {
        return new ServerResponse(_userService.GetById(id), null, 200);
    }

    public ServerResponse Save(CreateUserRequest request)
    {
       _userService.Save(request);
       return new ServerResponse(null, "User has been created!", 201);
    }

    public ServerResponse Update(UpdateUserRequest request)
    {
        try
        {
            var success = _userService.Update(request);
            if (success) return new ServerResponse(null, "User has been updated!", 200);
            return new ServerResponse(null, "User update failed. User not found or email might already exist.", 400);
        }
        catch (Exception ex)
        {
            return new ServerResponse(null, "User update failed due to an internal error.", 500, ex.Message);
        }
    }

    public ServerResponse DeleteById(int id)
    {
        try
        {
            var success = _userService.DeleteById(id);
            if (success) return new ServerResponse(null, "User has been deleted!", 200);
            return new ServerResponse(null, "User deletion failed. User not found.", 404);
        }
        catch (Exception ex)
        {
            return new ServerResponse(null, "User deletion failed due to an internal error.", 500, ex.Message);
        }
    }
}