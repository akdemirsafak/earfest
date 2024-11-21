namespace earfest.API.Base;

public class AppResult<T> where T : class
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public List<string>? Error { get; set; }

}
