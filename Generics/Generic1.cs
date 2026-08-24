public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Data { get; }
    public string? Error { get; }

    private Result(bool isSuccess, T? data, string? error)
    {
        IsSuccess = isSuccess;
        Data = data;
        Error = error;
    }

    // если очень хочется async-фабрику
    public static Task<Result<T>> SuccessAsync(T data)
        => Task.FromResult(new Result<T>(true, data, null));

    public static Task<Result<T>> FailureAsync(string error)
        => Task.FromResult(new Result<T>(false, default, error));
}