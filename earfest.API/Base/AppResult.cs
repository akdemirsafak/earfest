using System.Text.Json.Serialization;

namespace earfest.API.Base;

public class AppResult<T>
{
    //public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }

    [JsonIgnore] public int StatusCode { get; set; }

    //Static Factory Method
    public static AppResult<T> Success(T Data, int statusCode = 200)
    {
        return new AppResult<T> { Data = Data, StatusCode = statusCode };
    }

    public static AppResult<T> Success(int statusCode = 200)
    {
        return new AppResult<T> { Data = default, StatusCode = statusCode };
    }

    public static AppResult<T> Fail(List<string> errors, int statusCode = 0)
    {
        return new AppResult<T> { Data = default, Errors = errors, StatusCode = statusCode };
    }

    public static AppResult<T> Fail(string error, int statusCode = 0)
    {
        return new AppResult<T> { Data = default, Errors = new List<string> { error }, StatusCode = statusCode };
    }
}
