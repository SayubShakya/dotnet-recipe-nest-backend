using MessagePack;

namespace RecipeNest.Request;

[MessagePackObject]
public class CreateFavoriteRequest
{
    [Key("is_favorite")] public bool IsFavourite { get; set; }

    [Key("recipe_id")] public int RecipeId { get; set; }
}