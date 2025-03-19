using System.Net;
using MessagePack;
using RecipeNest.Controller;
using Repository;

namespace Router

{

    public class APIRouter
    {
        private RoleController roleController;

        public APIRouter(RoleController roleController)
        {
            this.roleController = roleController;
        }

        public string Route(HttpListenerRequest request)
        {
            string path = request.Url.AbsolutePath;

            if (path.StartsWith("/api/rest/"))
            {

                if (path.Contains("/api/rest/roles"))
                {
                    return Role(request);
                }

                if (path.Contains("/api/rest/users"))
                {
                    return User(request);
                }
            }

            return "404 Not Found";
        }

        public String Role(HttpListenerRequest request)
        {
            return roleController.GetAll();
        }

        public String User(HttpListenerRequest request)
        {
            return "User";
        }
    }
}