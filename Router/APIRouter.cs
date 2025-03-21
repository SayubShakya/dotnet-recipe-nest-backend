using System.Net;
using System.Text.RegularExpressions;
using RecipeNest.Controller;
using RecipeNest.Reponse;
using RecipeNest.Request;


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
            try
            {
                string path = request.Url.AbsolutePath + request.Url.Query;
                Console.WriteLine("requesting path: " + path);


                if (path.StartsWith("/api/rest/"))
                {
                    path = path.Replace("/api/rest", "");

                    if (path.Contains("/roles"))
                    {
                        return Role(path, request);
                    }

                    if (path.Contains("/users"))
                    {
                        return User(request);
                    }
                }

                return NotFound();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BaseController.ToJsonResponse(new ServerResponse(null, "Internal Server Error", 500, e.Message));
            }
        }

        public String Role(string path, HttpListenerRequest request)
        {
            Console.WriteLine("requesting path: " + path);

            if (Regex.IsMatch(path, @"^/roles/?$"))
            {
                if (request.HttpMethod.Equals("GET"))
                {
                    return roleController.GetAll();
                }
                else if (request.HttpMethod.Equals("POST"))
                {
                   return roleController.Save(BaseController.JsonRequestBody<CreateRoleRequest>(request));
                }
                else if (request.HttpMethod.Equals("PUT"))
                {
                    return roleController.Update(BaseController.JsonRequestBody<UpdateRoleRequest>(request));
                }
            }
            else if (Regex.IsMatch(path, @"^/roles\?id=\d+$"))
            {
                int id =  Convert.ToInt32(request.QueryString["id"]);
                
                if (request.HttpMethod.Equals("GET"))
                {
                    return roleController.GetById(id);
                } else if (request.HttpMethod.Equals("DELETE"))
                {
                    return roleController.DeleteById(id);
                }
            }

            return NotFound();
        }

        public String User(HttpListenerRequest request)
        {
            return "User";
        }

        public String NotFound()
        {
            return BaseController.ToJsonResponse(new ServerResponse(null, "404 Not Found", 404));
        }
    }
}