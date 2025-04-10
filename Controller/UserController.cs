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
        try
        {
            PaginatedResponse<UserResponse> response = _userService.GetAll(start, limit);
            return new ServerResponse(response, null, 200);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error occured while getting all users", ex);
        }
    }

    public string GetById(int id)
    {
        try
        {
            var userResponse = _userService.GetById(id);
            if (userResponse != null)
            {
                var serverResponse = new ServerResponse(userResponse, "User found!", 200);
                return ToJsonResponse(serverResponse);
            }
            else
            {
                var serverResponse = new ServerResponse(null, "User not found.", 404);
                return ToJsonResponse(serverResponse);
            }
        }
        catch (Exception ex)
        {
            return ToJsonResponse(new ServerResponse(null, "Failed to retrieve user.", 500, ex.Message));
        }
    }

    public string Save(CreateUserRequest request)
    {
        try
        {
            var success = _userService.Save(request);
            if (success) return ToJsonResponse(new ServerResponse(null, "User has been created!", 201));

            return ToJsonResponse(new ServerResponse(null, "User creation failed. Email might already exist.",
                400));
        }
        catch (Exception ex)
        {
            return ToJsonResponse(new ServerResponse(null, "User creation failed due to an internal error.", 500,
                ex.Message));
        }
    }

    public string Update(UpdateUserRequest request)
    {
        try
        {
            var success = _userService.Update(request);
            if (success) return ToJsonResponse(new ServerResponse(null, "User has been updated!", 200));

            return ToJsonResponse(new ServerResponse(null,
                "User update failed. User not found or email might already exist.", 400));
        }
        catch (Exception ex)
        {
            return ToJsonResponse(new ServerResponse(null, "User update failed due to an internal error.", 500,
                ex.Message));
        }
    }

    public string DeleteById(int id)
    {
        try
        {
            var success = _userService.DeleteById(id);
            if (success) return ToJsonResponse(new ServerResponse(null, "User has been deleted!", 200));

            return ToJsonResponse(new ServerResponse(null, "User deletion failed. User not found.", 404));
        }
        catch (Exception ex)
        {
            return ToJsonResponse(new ServerResponse(null, "User deletion failed due to an internal error.", 500,
                ex.Message));
        }
    }


    public string GetByEmail(string email)
    {
        try
        {
            Console.WriteLine($"Searching for email: '{email}'");
            var userResponse = _userService.GetByEmail(email);
            if (userResponse != null)
            {
                var serverResponse = new ServerResponse(userResponse, "User found!", 200);
                return ToJsonResponse(serverResponse);
            }
            else
            {
                var serverResponse = new ServerResponse(null, "User not found.", 404);
                return ToJsonResponse(serverResponse);
            }
        }
        catch (Exception ex)
        {
            return ToJsonResponse(new ServerResponse(null, "Failed to retrieve user.", 500, ex.Message));
        }
    }
}