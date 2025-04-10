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

    public ServerResponse GetById(int id)
    {
        try
        {
            var userResponse = _userService.GetById(id);
            if (userResponse != null)
            {
                return new ServerResponse(userResponse, "User found!", 200);
            }
            else
            {
                return new ServerResponse(null, "User not found.", 404);
            }
        }
        catch (Exception ex)
        {
            return new ServerResponse(null, "Failed to retrieve user.", 500, ex.Message);
        }
    }

    public ServerResponse Save(CreateUserRequest request)
    {
        try
        {
            var success = _userService.Save(request);
            if (success) return new ServerResponse(null, "User has been created!", 201);

            return new ServerResponse(null, "User creation failed. Email might already exist.", 400);
        }
        catch (Exception ex)
        {
            return new ServerResponse(null, "User creation failed due to an internal error.", 500, ex.Message);
        }
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
    
    public ServerResponse GetByEmail(string email)
    {
        try
        {
            Console.WriteLine($"Searching for email: '{email}'");
            var userResponse = _userService.GetByEmail(email);
            if (userResponse != null)
            {
                return new ServerResponse(userResponse, "User found!", 200);
            }
            else
            {
                return new ServerResponse(null, "User not found.", 404);
            }
        }
        catch (Exception ex)
        {
            return new ServerResponse(null, "Failed to retrieve user.", 500, ex.Message);
        }
    }
}