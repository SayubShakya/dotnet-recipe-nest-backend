using MessagePack;

namespace RecipeNest.Entity;

public class Cuisine
{
    public Cuisine()
    {
    }

    public Cuisine(int id, string name, string? imageUrl)
    {
        Id = id;
        Name = name;
        ImageUrl = imageUrl;
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public string? ImageUrl { get; set; }

    public override string ToString()
    {
        return $"Cuisine ID: {Id}, Name: {Name}, ImageUrl: {ImageUrl}";
    }
}