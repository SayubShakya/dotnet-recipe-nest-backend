namespace RecipeNest.CustomException;

public class CustomApplicationException: Exception
{
    public int StatusCode { get; set; }

    public CustomApplicationException(int statusCode, string? message, Exception? innerException) : base(message, innerException)
    {
        this.StatusCode = statusCode;
    }
}