namespace AuthService.Application.Data.DTOs.Response;

public class Result
{
    public string Message { get; }
    public bool IsSuccess { get; }
    public int StatusCode { get; }

    private Result(string message, bool isSucces, int statusCode)
    {
        Message = message;
        IsSuccess = isSucces;
        StatusCode = statusCode;
    }

    public static Result Success(string message = "Success", int statusCode = 200)
    {
        return new Result(message, true, statusCode);
    }
    
    public static Result Error(string message = "Error", int statusCode = 400)
    {
        return new Result(message, true, statusCode); //вы передаете true, значит IsSucces будет true даже при ошибке
    }
}