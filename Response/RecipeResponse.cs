// Reponse/RecipeResponse.cs

using MessagePack;

namespace RecipeNest.Reponse;

[MessagePackObject]
public class RecipeResponse
{
    public RecipeResponse()
    {
    }

    public RecipeResponse(int id, string? imageUrl, string title, string? description, string recipeDetail,
        string ingredients, int? recipeByUserId, int? cuisineId)
    {
        Id = id;
        ImageUrl = imageUrl;
        Title = title;
        Description = description;
        RecipeDetail = recipeDetail;
        Ingredients = ingredients;
        RecipeByUserId = recipeByUserId;
        CuisineId = cuisineId;
    }

    [Key("id")] public int Id { get; set; }

    [Key("image_url")] public string? ImageUrl { get; set; }

    [Key("title")] public string Title { get; set; }

    [Key("description")] public string? Description { get; set; }

    [Key("recipe")] public string RecipeDetail { get; set; }

    [Key("ingredients")] public string Ingredients { get; set; }

    [Key("recipe_by")] public int? RecipeByUserId { get; set; }

    [Key("cuisine")] public int? CuisineId { get; set; }

    public override string ToString()
    {
        return $"RecipeResponse ID: {Id}, Title: {Title}";
    }
}