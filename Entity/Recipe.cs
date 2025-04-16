// Model/Recipe.cs

namespace RecipeNest.Entity;

public class Recipe
{
    public Recipe()
    {
    }

    public Recipe(int id, string? imageUrl, string title, string? description, string recipeDetail,
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

    public int Id { get; set; }

    public string? ImageUrl { get; set; }

    public string Title { get; set; }

    public string? Description { get; set; }

    public string RecipeDetail { get; set; }

    public string Ingredients { get; set; }

    public int? RecipeByUserId { get; set; }

    public int? CuisineId { get; set; }

    public override string ToString()
    {
        return $"Recipe ID: {Id}, Title: {Title}";
    }
}