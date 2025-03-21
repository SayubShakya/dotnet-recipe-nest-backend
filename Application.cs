using System.Net;
using System.Text;

using Router;

using RecipeNest.Controller;
using RecipeNest.Service;
using RecipeNest.Db;
using RecipeNest.Repository;
using RecipeNest.Repository.Impl.Database;
using System.Text.RegularExpressions;

class Application
{
    static void Main()
    {

        string url = "http://localhost:9000/";

        HttpListener listener = new HttpListener();

        listener.Prefixes.Add(url);

        listener.Start();

        Console.WriteLine("Listening on " + url);

        IRoleRepository roleRepository = new RoleRepositoryDatabaseImpl();

        RoleService roleService = new RoleService(roleRepository);

        RoleController roleController = new RoleController(roleService);

        APIRouter router = new APIRouter(roleController);

        while (true)
        {
            try
            {
                Console.WriteLine($"Thread Name: {Thread.CurrentThread.Name ?? "No Name"}, Thread ID: {Thread.CurrentThread.ManagedThreadId}");
                // Wait for a new incoming request
                HttpListenerContext context = listener.GetContext();

                // Handle the request in a separate thread
                ThreadPool.QueueUserWorkItem((object state) =>
                {
                    HttpListenerResponse? response = null;

                    Console.WriteLine($"Thread Name: {Thread.CurrentThread.Name ?? "No Name"}, Thread ID: {Thread.CurrentThread.ManagedThreadId}");

                    HttpListenerContext context = (HttpListenerContext)state;
                    HttpListenerRequest? request = context.Request;
                    response = context.Response;

                    // Process the request and generate a response
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
        // Convert the response string to byte array
        byte[] buffer = Encoding.UTF8.GetBytes(responseString);
        response.ContentLength64 = buffer.Length;
        response.ContentType = "application/json";

        // Write the response
        response.OutputStream.Write(buffer, 0, buffer.Length);

        // Close the response stream
        response.OutputStream.Close();
    }
}