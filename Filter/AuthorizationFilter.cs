using System.Net;
using System.Security.Claims;
using Autofac;
using RecipeNest.CustomException;
using RecipeNest.Dto;
using RecipeNest.Entity;
using RecipeNest.Repository.Impl.Database;
using RecipeNest.Util.Impl;

namespace RecipeNest.Filter;

public class AuthorizationFilter
{
    public static bool Filter(HttpListenerRequest request, ILifetimeScope scope)
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

            var userRepository = scope.Resolve<UserRepositoryDatabaseImpl>();

            User? user = userRepository.GetByEmail(claimsMap["_email"]);

            if (user == null)
            {
                throw new CustomApplicationException(401, "Unauthorized", null);
            }

            var roleRepository = scope.Resolve<RoleRepositoryDatabaseImpl>();
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
}