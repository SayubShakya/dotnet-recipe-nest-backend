using System;
using System.Net;
using System.Text;

using Router;

using RecipeNest.Controller;
using RecipeNest.Service;
using RecipeNest.Repository;
using RecipeNest.Repository.Impl.Database;
using RecipeNest.Util;
using RecipeNest.Util.Impl;

class Application
{
    static void Main()
    {

        string url = "http://localhost:9000/";

        HttpListener listener = new HttpListener();

        listener.Prefixes.Add(url);

        listener.Start();

        Console.WriteLine("Listening on " + url);
        IHashingUtil hashingUtil = new BcryptUtil();
        String password = "Password1";
        String hashed = hashingUtil.Hash(password);
        
        Console.WriteLine("Check Password: " + hashingUtil.Verify(hashed,password ));
        
        Console.WriteLine($"Hashed: {hashed}");
        IRoleRepository roleRepository = new RoleRepositoryDatabaseImpl();
        IUserRepository userRepository = new UserRepositoryDatabaseImpl();
        ICuisineRepository cuisineRepository = new CuisineRepositoryDatabaseImpl();
        IRecipeRepository recipeRepository = new RecipeRepositoryDatabaseImpl();


        RoleService roleService = new RoleService(roleRepository);
        UserService userService = new UserService(userRepository, hashingUtil);
        CuisineService cuisineService = new CuisineService(cuisineRepository);
        RecipeService recipeService = new RecipeService(recipeRepository);
        

        RoleController roleController = new RoleController(roleService);
        UserController userController = new UserController(userService);
        CuisineController cuisineController = new CuisineController(cuisineService);
        RecipeController recipeController = new RecipeController(recipeService);

        APIRouter router = new APIRouter(roleController, userController, cuisineController, recipeController);

        while (true)
        {
            try
            {
                Console.WriteLine($"Thread Name: {Thread.CurrentThread.Name ?? "No Name"}, Thread ID: {Thread.CurrentThread.ManagedThreadId}");
                HttpListenerContext context = listener.GetContext();

                ThreadPool.QueueUserWorkItem((object state) =>
                {
                    HttpListenerResponse? response = null;

                    Console.WriteLine($"Thread Name: {Thread.CurrentThread.Name ?? "No Name"}, Thread ID: {Thread.CurrentThread.ManagedThreadId}");

                    HttpListenerContext context = (HttpListenerContext)state;
                    HttpListenerRequest? request = context.Request;
                    response = context.Response;

                    string responseString = router.Route(request);

                    ResponseBuilder(response, responseString);

                }, context);

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }

    private static void ResponseBuilder(HttpListenerResponse response, string responseString)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(responseString);
        response.ContentLength64 = buffer.Length;
        response.ContentType = "application/json";

        response.OutputStream.Write(buffer, 0, buffer.Length);

        response.OutputStream.Close();
    }
}