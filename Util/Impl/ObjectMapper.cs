using MessagePack;

namespace RecipeNest.Util.Impl;

public class ObjectMapper
{
    public static string ToJson(object objectz)
    {
        return MessagePackSerializer.SerializeToJson(objectz);
    }

    public static T ToObject<T>(string json)
    {
        return MessagePackSerializer.Deserialize<T>(MessagePackSerializer.ConvertFromJson(json));
    }
}