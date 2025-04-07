// RoleController.cs

using RecipeNest.Model;
using RecipeNest.Reponse;
using RecipeNest.Request;
using RecipeNest.Service;

namespace RecipeNest.Controller
{
    public class RoleController : BaseController
    {
        private RoleService roleService;

        public RoleController(RoleService roleService)
        {
            this.roleService = roleService;
        }

        public string GetAll()
        {
            ServerResponse serverRespose = new ServerResponse(roleService.GetAll(), "Role list!", 200);
            return ToJsonResponse(serverRespose);
        }

        public string GetById(int id)
        {
            ServerResponse serverRespose = new ServerResponse(roleService.GetById(id), "Role found!", 200);
            return ToJsonResponse(serverRespose);
        }

        public string Save(CreateRoleRequest request)
        {
            bool success = roleService.Save(request);
            if (success)
            {
                return ToJsonResponse(new ServerResponse(null, "Role has been created!", 201));
            }
            else
            {
                return ToJsonResponse(new ServerResponse(null, "Role creation failed!", 400));
            }
        }

        public string Update(UpdateRoleRequest request)
        {
            bool success = roleService.Update(request);
            if (success)
            {
                return ToJsonResponse(new ServerResponse(null, "Role has been updated!", 200));
            }
            else
            {
                return ToJsonResponse(new ServerResponse(null, "Role has update failed!", 400));
            }
        }

        public string DeleteById(int id)
        {
            ServerResponse serverRespose = new ServerResponse(roleService.DeleteById(id), "Role deleted!", 200);
            return ToJsonResponse(serverRespose);
        }
    }
}