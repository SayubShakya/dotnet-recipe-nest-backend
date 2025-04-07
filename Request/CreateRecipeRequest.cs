// Request/CreateRecipeRequest.cs

using MessagePack;

namespace RecipeNest.Request
{
    [MessagePackObject]
    public class CreateRecipeRequest
    {
        [Key("image_url")] public string? ImageUrl { get; set; }

        [Key("title")] public string Title { get; set; }

        [Key("description")] public string? Description { get; set; }

        [Key("recipe")] public string RecipeDetail { get; set; }

        [Key("ingredients")] public string Ingredients { get; set; }

        [Key("recipe_by")] public int? RecipeByUserId { get; set; }

        [Key("cuisine")] public int? CuisineId { get; set; }

        public CreateRecipeRequest()
        {
        }

        public CreateRecipeRequest(string title, string recipeDetail, string ingredients, string? imageUrl = null,
            string? description = null, int? recipeByUserId = null, int? cuisineId = null)
        {
            ImageUrl = imageUrl;
            Title = title;
            Description = description;
            RecipeDetail = recipeDetail;
            Ingredients = ingredients;
            RecipeByUserId = recipeByUserId;
            CuisineId = cuisineId;
        }

        public override string ToString()
        {
            return $"CreateRecipeRequest: Title={Title}, CuisineId={CuisineId}, UserId={RecipeByUserId}";
        }
    }
}