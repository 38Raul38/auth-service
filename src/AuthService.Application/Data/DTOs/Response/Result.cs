namespace AuthService.Application.Data.DTOs.Response;

public class Result
{
    public string Message { get; }
    public bool IsSucces { get; }
    public int StatusCode { get; }

    private Result(string message, bool isSucces, int statusCode)
    {
        Message = message;
        IsSucces = isSucces;
        StatusCode = statusCode;
    }

    public static FluentResults.Result Success(string message = "Success", int statusCode = 200)
    {
        return new Result(message, true, statusCode);
    }
    
    public static Result Error(string message = "Error", int statusCode = 400)
    {
        return new Result(message, true, statusCode);
    }
}