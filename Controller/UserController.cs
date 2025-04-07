// UserController.cs

using RecipeNest.Reponse; 
using RecipeNest.Request; 
using RecipeNest.Service; 
using RecipeNest.Model;   

namespace RecipeNest.Controller
{
    public class UserController : BaseController
    {
        private UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        public string GetAll()
        {
            try
            {
                ServerResponse serverResponse = new ServerResponse(userService.GetAll(), "User list!", 200);
                return ToJsonResponse(serverResponse);
            }
            catch (System.Exception ex)
            {
                return ToJsonResponse(new ServerResponse(null, "Failed to retrieve users.", 500, ex.Message));
            }
        }

        public string GetById(int id)
        {
            try
            {
                var userResponse = userService.GetById(id); 
                if (userResponse != null)
                {
                    ServerResponse serverResponse = new ServerResponse(userResponse, "User found!", 200);
                    return ToJsonResponse(serverResponse);
                }
                else
                {
                    ServerResponse serverResponse = new ServerResponse(null, "User not found.", 404);
                    return ToJsonResponse(serverResponse);
                }
            }
            catch (System.Exception ex)
            {
                return ToJsonResponse(new ServerResponse(null, "Failed to retrieve user.", 500, ex.Message));
            }
        }

        public string Save(CreateUserRequest request)
        {
            try
            {
                bool success = userService.Save(request);
                if (success)
                {
                    return ToJsonResponse(new ServerResponse(null, "User has been created!", 201));
                }
                else
                {
                    return ToJsonResponse(new ServerResponse(null, "User creation failed. Email might already exist.", 400)); 
                }
            }
             catch (System.Exception ex) 
            {
                return ToJsonResponse(new ServerResponse(null, "User creation failed due to an internal error.", 500, ex.Message));
            }
        }

        public string Update(UpdateUserRequest request)
        {
             try
            {
                bool success = userService.Update(request);
                if (success)
                {
                    return ToJsonResponse(new ServerResponse(null, "User has been updated!", 200)); 
                }
                else
                {
                     return ToJsonResponse(new ServerResponse(null, "User update failed. User not found or email might already exist.", 400)); 
                }
            }
             catch (System.Exception ex) 
            {
                return ToJsonResponse(new ServerResponse(null, "User update failed due to an internal error.", 500, ex.Message));
            }
        }

        public string DeleteById(int id)
        {
             try
            {
                bool success = userService.DeleteById(id);
                if (success)
                {
                    return ToJsonResponse(new ServerResponse(null, "User has been deleted!", 200)); 
                }
                else
                {
                    return ToJsonResponse(new ServerResponse(null, "User deletion failed. User not found.", 404)); 
                }
            }
            catch (System.Exception ex)
            {
                return ToJsonResponse(new ServerResponse(null, "User deletion failed due to an internal error.", 500, ex.Message));
            }
        }
        
        
        public string GetByEmail(string email)
        {
            try
            {
                Console.WriteLine($"Searching for email: '{email}'");
                var userResponse = userService.GetByEmail(email); 
                if (userResponse != null)
                {
                    ServerResponse serverResponse = new ServerResponse(userResponse, "User found!", 200);
                    return ToJsonResponse(serverResponse);
                }
                else
                {
                    ServerResponse serverResponse = new ServerResponse(null, "User not found.", 404);
                    return ToJsonResponse(serverResponse);
                }
            }
            catch (System.Exception ex)
            {
                return ToJsonResponse(new ServerResponse(null, "Failed to retrieve user.", 500, ex.Message));
            }
        }

    }
}