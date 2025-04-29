using MessagePack;

namespace RecipeNest.Response;

[MessagePackObject]
public class PaginatedResponse<T>
{
    public PaginatedResponse()
    {
    }

    [Key("items")] public List<T>? Items { get; set; }
    [Key("start")] public int Start { get; set; }
    [Key("limit")] public int Limit { get; set; }
    [Key("count")] public int Count { get; set; }
}