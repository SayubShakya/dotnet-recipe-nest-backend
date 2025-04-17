using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using Autofac;
using DotNetEnv;
using RecipeNest.Dto;
using RecipeNest.Entity;
using RecipeNest.Repository.Impl.Database;
using RecipeNest.Response;
using RecipeNest.Router;
using RecipeNest.Util.Impl;

namespace RecipeNest;

internal class Application
{
    private static void Main()
    {
        Env.Load();
        var container = GetContainer();
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
            .InstancePerLifetimeScope()
            .AsSelf();

        builder.RegisterType<SessionUser>().InstancePerLifetimeScope();
        var container = builder.Build();
        return container;
    }

    private static void HandleRequest(IContainer container, object? state)
    {
        Console.WriteLine(
            $"Thread Name: {Thread.CurrentThread.Name ?? "No Name"}, Thread ID: {Environment.CurrentManagedThreadId}");

        HttpListenerResponse? response = null;
        var context = (HttpListenerContext)state!;
        var request = context.Request;
        response = context.Response;

        if (request.HttpMethod == "OPTIONS")
        {
            response.StatusCode = 204;
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Headers.Add("Access-Control-Allow-Credentials", "true");
            response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            response.Headers.Add("Access-Control-Allow-Headers", "*");
            response.Headers.Add("Access-Control-Max-Age", "86400");
            response.OutputStream.Close();
            return;
        }

        try
        {
            using (var scope = container.BeginLifetimeScope())
            {
                var router = scope.Resolve<APIRouter>();
                var userRepository = scope.Resolve<UserRepositoryDatabaseImpl>();
                var roleRepository = scope.Resolve<RoleRepositoryDatabaseImpl>();

                bool isAuthorized = AuthorizationFilter(request, userRepository, response, roleRepository, scope);

                if (!isAuthorized) return;

                ServerResponse serverResponse = router.Route(request);
                ResponseBuilder(response, serverResponse);
            }
        }
        catch (Exception e)
        {
            ResponseBuilder(response, ResponseUtil.ServerError(e.Message));
        }
    }

    private static bool AuthorizationFilter(HttpListenerRequest request, UserRepositoryDatabaseImpl userRepository,
        HttpListenerResponse response, RoleRepositoryDatabaseImpl roleRepository, ILifetimeScope scope)
    {
        string? token = request.Headers["Authorization"];

        if (token == null)
        {
            return true;
        }

        if (token.StartsWith("Bearer "))
        {
            token = token.Replace("Bearer ", "");
        }
        else
        {
            return false;
        }

        if (token.Length > 0)
        {
            ClaimsPrincipal claimsPrincipal = TokenUtil.ValidateToken(token);
            Dictionary<string, string> claimsMap = claimsPrincipal.Claims
                .ToDictionary(c => c.Type, c => c.Value);

            User? user = userRepository.GetByEmail(claimsMap["_email"]);

            if (user == null)
            {
                ResponseBuilder(response, ResponseUtil.Unauthorized());
                return false;
            }

            Role role = roleRepository.GetById(user.RoleId);

            BuildSessionUser(scope, user, role);

            return true;
        }

        return true;
    }

    private static void BuildSessionUser(ILifetimeScope scope, User user, Role role)
    {
        SessionUser sessionUser = scope.Resolve<SessionUser>();
        sessionUser.User = user;
        sessionUser.Role = role;
        sessionUser.Authenticated = true;
    }

    private static void ResponseBuilder(HttpListenerResponse response, ServerResponse serverResponse)
    {
        var buffer = Encoding.UTF8.GetBytes(ObjectMapper.ToJson(serverResponse));
        response.ContentLength64 = buffer.Length;
        response.StatusCode = serverResponse.StatusCode;
        response.ContentType = "application/json";
        response.Headers.Add("Access-Control-Allow-Origin", "*");
        response.Headers.Add("Access-Control-Allow-Credentials", "true");
        response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
        response.Headers.Add("Access-Control-Allow-Headers", "*");
        response.Headers.Add("Access-Control-Max-Age", "86400");
        response.OutputStream.Write(buffer, 0, buffer.Length);
        response.OutputStream.Close();
    }
}