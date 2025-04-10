//ServerResponse.cs

using MessagePack;

namespace RecipeNest.Response;

[MessagePackObject]
public class ServerResponse
{
    public ServerResponse()
    {
    }

    public ServerResponse(object? objectz = null, string? msg = "Internal Server Error", int statusCode = 500,
        string? detail = "")
    {
        Object = objectz;
        Message = msg;
        StatusCode = statusCode;
        Detail = detail;
    }

    [Key("data")] public object? Object { get; set; }

    [Key("message")] public string? Message { get; set; }

    [Key("detail")] public string? Detail { get; set; }

    [Key("statusCode")] public int StatusCode { get; set; }
}