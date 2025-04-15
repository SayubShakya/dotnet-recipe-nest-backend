namespace RecipeNest.CustomException;

public class CustomApplicationException: System.Exception
{
    public int StatusCode { get; set; }

    public CustomApplicationException(int statusCode, string? message, System.Exception? innerException) : base(message, innerException)
    {
        this.StatusCode = statusCode;
    }
}