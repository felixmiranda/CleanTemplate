namespace CleanTemplate.Application.Commons.Bases;

public class BaseResponse<T>
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = null!;
    public T? Data { get; set; }
    public int TotalRecords { get; set; }
}
