namespace AuthService.Application.Data.DTOs.Response;

public class TypeResult<T>
{
    public string Message { get; }
    public bool IsSucces { get; }
    public int StatusCode { get; }
    public T? Data { get; }

    private TypeResult(string message, bool isSucces, int statusCode, T? data)
    {
        Message = message;
        IsSucces = isSucces;
        StatusCode = statusCode;
        Data = data;
    }

    public static TypeResult<T> Success(string message = "Success", int statusCode = 200, T? data = default)
    {
        return new TypeResult<T>(message, true, statusCode,  data);
    }
    
    public static TypeResult<T> Error(string message = "Error", int statusCode = 400, T? data = default)
    {
        return new TypeResult<T>(message, true, statusCode,  data);
    }
}