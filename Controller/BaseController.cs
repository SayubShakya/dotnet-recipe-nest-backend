using System.Net;
using System.Text;
using MessagePack;
using RecipeNest.Util.Impl;

namespace RecipeNest.Controller;

public class BaseController
{
    private static string RequestBody(HttpListenerRequest request)
    {
        using var reader = new StreamReader(request.InputStream, Encoding.UTF8);
        return reader.ReadToEnd();
    }

    public static T JsonRequestBody<T>(HttpListenerRequest request)
    {
        var requestBody = RequestBody(request);
        return ObjectMapper.ToObject<T>(requestBody);
    }
}