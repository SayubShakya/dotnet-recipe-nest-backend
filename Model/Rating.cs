using MessagePack;

namespace RecipeNest.Model;

public enum RatingScore : int
{
    Zero = 0,
    One = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9,
    Ten = 10
}

[MessagePackObject]
public class Rating
{
    public Rating()
    {
    }

    public Rating(int id, RatingScore? score, int? recipeId, int? userId)
    {
        Id = id;
        Score = score;
        RecipeId = recipeId;
        UserId = userId;
    }

    [Key("id")] public int Id { get; set; }

    [Key("rating")] public RatingScore? Score { get; set; }

    [Key("recipe_id")] public int? RecipeId { get; set; }

    [Key("user_id")] public int? UserId { get; set; }

    public override string ToString()
    {
        return
            $"Rating ID: {Id}, UserID: {UserId?.ToString() ?? "NULL"}, RecipeID: {RecipeId?.ToString() ?? "NULL"}, Score: {Score?.ToString() ?? "NULL"}";
    }
}