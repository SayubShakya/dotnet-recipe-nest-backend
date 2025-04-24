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
        return new ServerResponse(_userService.GetActiveById(id), null, 200);
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

    public ServerResponse UpdateProfile(UpdateUserProfileRequest request)
    {
        var success = _userService.UpdateProfile(request);
        if (success) return new ServerResponse(null, "Your profile has been updated!", 200);
        return new ServerResponse(null, "Updating your profile failed", 400);
    }

    public ServerResponse ToggleUserActivation(ToggleUserStatusRequest request)
    {
        if (request.IsActive)
        {
            var success = _userService.Activate(request.Id);
            if (success) return new ServerResponse(null, "User has been activated!", 200);
            return new ServerResponse(null, "User deactivation failed", 500);
        }
        else
        {
            var success = _userService.Deactivate(request.Id);
            if (success) return new ServerResponse(null, "User has been deactivated!", 200);
            return new ServerResponse(null, "User deactivation failed", 500);
        }
    }
}