using MessagePack;

namespace RecipeNest.Request;

[MessagePackObject]
public class CreateCuisineRequest
{
    public CreateCuisineRequest()
    {
    }

    public CreateCuisineRequest(string name, string? imageUrl)
    {
        Name = name;
        ImageUrl = imageUrl;
    }

    [Key("name")] public string Name { get; set; }

    [Key("image_url")] public string? ImageUrl { get; set; }

    public override string ToString()
    {
        return $"CreateCuisineRequest(Name='{Name}', ImageUrl='{ImageUrl}";
    }
}