using MessagePack;

namespace RecipeNest.Entity;

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

    public int Id { get; set; }

    public RatingScore? Score { get; set; }

    public int? RecipeId { get; set; }

    public int? UserId { get; set; }

    public override string ToString()
    {
        return
            $"Rating ID: {Id}, UserID: {UserId?.ToString() ?? "NULL"}, RecipeID: {RecipeId?.ToString() ?? "NULL"}, Score: {Score?.ToString() ?? "NULL"}";
    }
}