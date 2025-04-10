using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using Autofac;
using RecipeNest.Dto;
using RecipeNest.Model;
using RecipeNest.Repository.Impl.Database;
using RecipeNest.Response;
using RecipeNest.Router;
using RecipeNest.Util.Impl;

namespace RecipeNest;

internal class Application
{
    private static void Main()
    {
        var container = GetContainer();
        var listener = GetHttpListener();

        while (true)
        {
            try
            {
                var context = listener.GetContext();
                ThreadPool.QueueUserWorkItem(state => { HandleRequest(container, state); }, context);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }

    private static HttpListener GetHttpListener()
    {
        var url = "http://localhost:9000/";

        var listener = new HttpListener();

        listener.Prefixes.Add(url);

        listener.Start();

        Console.WriteLine("Listening on " + url);

        return listener;
    }

    private static IContainer GetContainer()
    {
        var builder = new ContainerBuilder();

        var dependencyScanPath = new List<string>
        {
            "RecipeNest.Controller",
            "RecipeNest.Db",
            "RecipeNest.Repository",
            "RecipeNest.Router",
            "RecipeNest.Service",
            "RecipeNest.Util"
        };

        builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .Where(t =>
            {
                var fullName = t.FullName;
                var pathFound = 0;

                foreach (var path in dependencyScanPath)
                    if (fullName != null)
                        if (fullName.StartsWith(path))
                        {
                            Console.WriteLine("Registering object for: " + fullName);
                            pathFound++;
                        }

                return pathFound > 0;
            })
            .AsImplementedInterfaces()
            .SingleInstance()
            .AsSelf();

        builder.RegisterType<SessionUserDTO>().InstancePerLifetimeScope();
        var container = builder.Build();
        return container;
    }

    private static void HandleRequest(IContainer container, object? state)
    {
        HttpListenerResponse? response = null;
        var context = (HttpListenerContext)state!;
        var request = context.Request;
        response = context.Response;

        var router = container.Resolve<APIRouter>();
        var userRepository = container.Resolve<UserRepositoryDatabaseImpl>();

        try
        {
            Console.WriteLine(
                $"Thread Name: {Thread.CurrentThread.Name ?? "No Name"}, Thread ID: {Environment.CurrentManagedThreadId}");

            Console.WriteLine("router: " + router.GetHashCode());

            string? token = request.Headers["Authorization"];

            if (token != null)
            {
                using var scope = container.BeginLifetimeScope();

                var sessionUserDto = scope.Resolve<SessionUserDTO>();
                Console.WriteLine("sessionUserDto: " + sessionUserDto.GetHashCode());

                ClaimsPrincipal claimsPrincipal = TokenUtil.ValidateToken(token);
                Dictionary<string, string> claimsMap = claimsPrincipal.Claims
                    .ToDictionary(c => c.Type, c => c.Value);
                User user = userRepository.GetByEmail(claimsMap["_email"]);
                sessionUserDto.User = user;
                sessionUserDto.Authenticated = true;
                var responseString = router.Route(request);

                ResponseBuilder(response, responseString);
            }
            else
            {
                var responseString = router.Route(request);
                ResponseBuilder(response, responseString);
            }
        }
        catch (Exception e)
        {
            ResponseBuilder(response, ResponseUtil.ServerError(e.Message));
        }
    }

    private static void ResponseBuilder(HttpListenerResponse response, ServerResponse serverResponse)
    {
        var buffer = Encoding.UTF8.GetBytes(ObjectMapper.ToJson(serverResponse));
        response.ContentLength64 = buffer.Length;
        response.StatusCode = serverResponse.StatusCode;
        response.ContentType = "application/json";
        response.OutputStream.Write(buffer, 0, buffer.Length);
        response.OutputStream.Close();
    }
}