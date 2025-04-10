namespace RecipeNest.CustomExceptionl;

public class ApplicationExceptionCustomeException: System.Exception
{
    public int StatusCode { get; set; }

    public ApplicationExceptionCustomeException(int statusCode, string? message, System.Exception? innerException) : base(message, innerException)
    {
        this.StatusCode = statusCode;
    }
}