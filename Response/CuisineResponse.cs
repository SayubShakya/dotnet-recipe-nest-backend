using MessagePack;

namespace RecipeNest.Response;

[MessagePackObject]
public class CuisineResponse
{
    public CuisineResponse()
    {
    }

    public CuisineResponse(int id, string name, string? imageUrl)
    {
        Id = id;
        Name = name;
        ImageUrl = imageUrl;
    }

    [Key("id")] public int Id { get; set; }

    [Key("name")] public string Name { get; set; }

    [Key("image_url")] public string? ImageUrl { get; set; }
}