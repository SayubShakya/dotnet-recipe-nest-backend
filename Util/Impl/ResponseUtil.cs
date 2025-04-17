using System.Net;
using System.Text;
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
    public static void ResponseBuilder(HttpListenerResponse response, ServerResponse serverResponse)
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