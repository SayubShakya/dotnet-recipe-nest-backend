using System.Reflection;
using Autofac;
using RecipeNest.Dto;

namespace RecipeNest.Config;

public class DIConfiguration
{
    public static IContainer GetContainer()
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
}