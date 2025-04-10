using RecipeNest.Response;

namespace RecipeNest.Util.Impl;

public class ResponseUtil
{
    public static ServerResponse NotFound()
    {
        Console.WriteLine("Returning 404 Not Found");
        return new ServerResponse(null, "404 Not Found", 404);
    }

    public static ServerResponse Unauthorized()
    {
        Console.WriteLine("Returning 401 Not Found");
        return new ServerResponse(null, "401 Unauthorized", 401);
    }

    public static ServerResponse ServerError(string message="500 Server Error")
    {
        Console.WriteLine("Returning ServerError");
        return new ServerResponse(null, message, 500);
    }
}