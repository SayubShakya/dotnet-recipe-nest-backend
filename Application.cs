using System;
using System.Net;
using System.Reflection;
using System.Text;
using Autofac;
using RecipeNest.Controller;
using RecipeNest.Service;
using RecipeNest.Repository;
using RecipeNest.Repository.Impl.Database;
using RecipeNest.Router;
using RecipeNest.Util;
using RecipeNest.Util.Impl;

class Application
{
    static void Main()
    {
        ContainerBuilder builder = new ContainerBuilder();

        List<string> dependencyPath = new List<string>
        {
            "RecipeNest.Controller",
            "RecipeNest.Db",
            "RecipeNest.Repository",
            "RecipeNest.Router",
            "RecipeNest.Service",
            "RecipeNest.Util"
        };


        // Register all types in this assembly
        builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .Where(t =>
            {
                string? fullName = t.FullName;
                int pathFound = 0;

                foreach (string path in dependencyPath)
                {
                    if (fullName != null)
                    {
                        if (fullName.StartsWith(path))
                        {
                            Console.WriteLine("Registering object for: " + fullName);
                            pathFound++;
                        }
                    }
                }

                return pathFound > 0;
            })
            .AsImplementedInterfaces()
            .AsSelf();

        IContainer container = builder.Build();
        APIRouter router = container.Resolve<APIRouter>();

        string url = "http://localhost:9000/";

        HttpListener listener = new HttpListener();

        listener.Prefixes.Add(url);

        listener.Start();

        Console.WriteLine("Listening on " + url);

        while (true)
        {
            try
            {
                Console.WriteLine(
                    $"Thread Name: {Thread.CurrentThread.Name ?? "No Name"}, Thread ID: {Thread.CurrentThread.ManagedThreadId}");
                HttpListenerContext context = listener.GetContext();

                ThreadPool.QueueUserWorkItem((object state) =>
                {
                    HttpListenerResponse? response = null;

                    Console.WriteLine(
                        $"Thread Name: {Thread.CurrentThread.Name ?? "No Name"}, Thread ID: {Thread.CurrentThread.ManagedThreadId}");

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