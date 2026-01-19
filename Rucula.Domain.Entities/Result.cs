namespace Rucula.Domain.Entities;

public readonly record struct Result<T>
{
    private readonly Exception? _exception;

    public bool IsSuccess { get; }
    public Exception Exception => _exception ?? throw new InvalidOperationException($"Se intentó acceder a la propiedad Exception nula cuando IsSuccess = {IsSuccess}");
    public T Value => IsSuccess ? field! : throw _exception!;

    private Result(T value) : this(value, default, true) { }
    private Result(Exception ex) : this(default, ex, false) { }
    private Result(T? value, Exception? ex, bool isSuccess)
    {
        Value = value;
        _exception = ex;
        IsSuccess = isSuccess;
    }

    public static Result<T> Success(T value)
        => new(value);
    public static Result<T> Failure(Exception exception)
        => new(exception);
}
