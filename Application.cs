using System.Net;
using System.Text;

using Router;
using Repository;
using Repository.Implementation;
using RecipeNest.Util.Database;
using RecipeNest.Controller;
using RecipeNest.Service;

class Application
{
    static void Main()
    {
        // Set the minimum and maximum number of threads in the thread pool
        int minThreads = 100;  // Minimum threads in the pool
        int maxThreads = 100; // Maximum threads in the pool

        // Set the thread pool size
        ThreadPool.SetMinThreads(minThreads, minThreads);
        ThreadPool.SetMaxThreads(maxThreads, maxThreads);

        // Create an HttpListener to listen for HTTP requests
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:9000/");

        // Start the listener
        listener.Start();
        Console.WriteLine("Listening on http://localhost:9000/");

        DatabaseUtil databaseUtil = new DatabaseUtil();

        RoleRepository roleRepository = new RoleRepositoryImpl(databaseUtil);

        RoleService roleService = new RoleService(roleRepository);

        RoleController roleController = new RoleController(roleService);

        APIRouter router = new APIRouter(roleController);

        while (true)
        {
            // Wait for a new incoming request
            HttpListenerContext context = listener.GetContext();

            // Handle the request in a separate thread
            ThreadPool.QueueUserWorkItem((object state) =>
            {
                Console.WriteLine($"Thread Name: {Thread.CurrentThread.Name ?? "No Name"}, Thread ID: {Thread.CurrentThread.ManagedThreadId}");

                HttpListenerContext context = (HttpListenerContext)state;
                HttpListenerRequest? request = context.Request;
                HttpListenerResponse? response = context.Response;

                // Process the request and generate a response
                string responseString = router.Route(request);
                // Convert the response string to byte array
                byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                response.ContentType = "application/json";

                // Write the response
                response.OutputStream.Write(buffer, 0, buffer.Length);

                // Close the response stream
                response.OutputStream.Close();

            }, context);
        }
    }
}