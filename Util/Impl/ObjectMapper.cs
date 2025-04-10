using MessagePack;

namespace RecipeNest.Util.Impl;

public class ObjectMapper
{
    public static string ToJson(object responseObject)
    {
        return MessagePackSerializer.SerializeToJson(responseObject);
    }
}