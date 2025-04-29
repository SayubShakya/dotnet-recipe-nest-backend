using System.Net;
using Autofac;
using DotNetEnv;
using RecipeNest.Config;
using RecipeNest.Filter;
using RecipeNest.Response;
using RecipeNest.Router;
using RecipeNest.Util.Impl;

namespace RecipeNest;

public static class Application
{
    private static void Main()
    {
        Env.Load();
        var container = DIConfiguration.GetContainer();
        var listener = GetHttpListener();

        while (true)
        {
            try
            {
                ThreadPool.QueueUserWorkItem(state => { HandleRequest(container, state); }, listener.GetContext());
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }

    private static HttpListener GetHttpListener()
    {
        string? url = Environment.GetEnvironmentVariable("APPLICATION_URL");
        string? port = Environment.GetEnvironmentVariable("APPLICATION_PORT");

        string? fullUrl = $"{url}:{port}/";

        Console.WriteLine($"Listening on: {fullUrl}");

        var listener = new HttpListener();

        listener.Prefixes.Add(fullUrl);

        listener.Start();

        return listener;
    }

    private static void HandleRequest(IContainer container, object? state)
    {
        Console.WriteLine(
            $"Thread Name: {Thread.CurrentThread.Name ?? "No Name"}, Thread ID: {Environment.CurrentManagedThreadId}");

        HttpListenerContext context = (HttpListenerContext)state!;

        HttpListenerRequest request = context.Request;
        HttpListenerResponse response = context.Response;

        if (HandleOptionRequest(request, response)) return;

        try
        {
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                APIRouter router = scope.Resolve<APIRouter>();

                bool isAuthorized =
                    AuthorizationFilter.Filter(request, scope);

                if (!isAuthorized) return;

                ServerResponse serverResponse = router.Route(request);
                ResponseUtil.ResponseBuilder(response, serverResponse);
            }
        }
        catch (Exception e)
        {
            ResponseUtil.ResponseBuilder(response, ResponseUtil.ServerError(e.Message));
        }
    }

    private static bool HandleOptionRequest(HttpListenerRequest request, HttpListenerResponse response)
    {
        if (request.HttpMethod == "OPTIONS")
        {
            response.StatusCode = 204;
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Headers.Add("Access-Control-Allow-Credentials", "true");
            response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            response.Headers.Add("Access-Control-Allow-Headers", "*");
            response.Headers.Add("Access-Control-Max-Age", "86400");
            response.OutputStream.Close();
            return true;
        }

        return false;
    }
}