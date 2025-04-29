// Reponse/RecipeResponse.cs

using MessagePack;

namespace RecipeNest.Response;

[MessagePackObject]
public class RecipeResponse
{
    public RecipeResponse()
    {
    }

    public RecipeResponse(int id, string? imageUrl, string title, string? description, string recipeDetail,
        string ingredients, int? recipeByUserId, int? cuisineId, bool? isFavorite, int? rating)
    {
        Id = id;
        ImageUrl = imageUrl;
        Title = title;
        Description = description;
        RecipeDetail = recipeDetail;
        Ingredients = ingredients;
        RecipeByUserId = recipeByUserId;
        CuisineId = cuisineId;
        IsFavorite = isFavorite;
        Rating = rating;
    }

    [Key("id")] public int Id { get; set; }

    [Key("image_url")] public string? ImageUrl { get; set; }

    [Key("title")] public string Title { get; set; }

    [Key("description")] public string? Description { get; set; }

    [Key("recipe")] public string RecipeDetail { get; set; }

    [Key("ingredients")] public string Ingredients { get; set; }

    [Key("recipe_by")] public int? RecipeByUserId { get; set; }

    [Key("cuisine_id")] public int? CuisineId { get; set; }
    
    [Key("is_favorite")] public bool? IsFavorite { get; set; }
    
    [Key("rating")] public int? Rating { get; set; }
}