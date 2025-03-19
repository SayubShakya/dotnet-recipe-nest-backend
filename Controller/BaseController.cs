using MessagePack;

namespace RecipeNest.Controller
{

    public class BaseController
    {
        protected String ToJsonResponse(object responseObject)
        {
            return MessagePackSerializer.SerializeToJson(responseObject);
        }
    }
}