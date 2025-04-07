//BaseController.cs

using System.IO;
using System.Net;
using System.Text;
using MessagePack;

namespace RecipeNest.Controller
{
    public class BaseController
    {
        public static string ToJsonResponse(object responseObject)
        {
            return MessagePackSerializer.SerializeToJson(responseObject);
        }

        public static string RequestBody(HttpListenerRequest request)
        {
            using StreamReader reader = new StreamReader(request.InputStream, Encoding.UTF8);
            return reader.ReadToEnd();
        }

        public static T JsonRequestBody<T>(HttpListenerRequest request)
        {
            string requestBody = RequestBody(request);
            return MessagePackSerializer.Deserialize<T>(MessagePackSerializer.ConvertFromJson(requestBody));
        }
    }
}