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

        public String GetAll()
        {
            return ToJsonResponse(roleService.GetAll());
        }
    }
}