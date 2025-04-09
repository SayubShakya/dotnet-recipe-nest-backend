using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using Autofac;
using RecipeNest.Dto;
using RecipeNest.Repository.Impl.Database;
using RecipeNest.Router;
using RecipeNest.Util.Impl;

internal class Application
{
    private static void Main()
    {
        Console.WriteLine(TokenUtil.GenerateToken("sah@gmail.com"));

        Console.WriteLine(new BcryptUtil().Hash("123123123"));

        var builder = new ContainerBuilder();

        var dependencyPath = new List<string>
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

                foreach (var path in dependencyPath)
                    if (fullName != null)
                        if (fullName.StartsWith(path))
                        {
                            Console.WriteLine("Registering object for: " + fullName);
                            pathFound++;
                        }

                return pathFound > 0;
            })
            .AsImplementedInterfaces()
            .AsSelf();
        
        // Register your services with InstancePerRequest
        builder.RegisterType<SessionUserDTO>();
        
        var container = builder.Build();
        var router = container.Resolve<APIRouter>();

        var url = "http://localhost:9000/";

        var listener = new HttpListener();

        listener.Prefixes.Add(url);

        listener.Start();

        Console.WriteLine("Listening on " + url);
        

        while (true)
            try
            {
                using (var scope = container.BeginLifetimeScope())
                {
                   var sessionUserDto =  scope.Resolve<SessionUserDTO>();
                    Console.WriteLine(
                        $"Thread Name: {Thread.CurrentThread.Name ?? "No Name"}, Thread ID: {Thread.CurrentThread.ManagedThreadId}");
                    
                    var context = listener.GetContext();

                    ThreadPool.QueueUserWorkItem(state =>
                    {
                        HttpListenerResponse? response = null;

                        Console.WriteLine(
                            $"Thread Name: {Thread.CurrentThread.Name ?? "No Name"}, Thread ID: {Thread.CurrentThread.ManagedThreadId}");

                        var context = (HttpListenerContext)state;
                        var request = context.Request;
                        response = context.Response;
                        

                        string? token = request.Headers["Authorization"];

                        if (token != null)
                        {
                            ClaimsPrincipal claimsPrincipal = TokenUtil.ValidateToken(token);

                            var email = claimsPrincipal.Claims.Where(x => x.Type == "_email").FirstOrDefault().Value;
                            var userRespository = container.Resolve<UserRepositoryDatabaseImpl>();
                            sessionUserDto.User = userRespository.GetByEmail(email);
                            sessionUserDto.Authenticated = true;
                        }


                        var responseString = router.Route(request);
                        

                        ResponseBuilder(response, responseString);
                    }, context);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
    }

    private static void ResponseBuilder(HttpListenerResponse response, string responseString)
    {
        var buffer = Encoding.UTF8.GetBytes(responseString);
        response.ContentLength64 = buffer.Length;
        response.ContentType = "application/json";

        response.OutputStream.Write(buffer, 0, buffer.Length);

        response.OutputStream.Close();
    }
}