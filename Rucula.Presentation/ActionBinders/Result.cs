namespace Rucula.Presentation.ActionBinders;

internal readonly record struct Result<T>
{
    private readonly Exception? _exception;

    internal bool IsSuccess { get; }
    internal T Value => IsSuccess ? field! : throw _exception!;

    private Result(T value) : this(value, default, true) { }
    private Result(Exception ex) : this(default, ex, false) { }
    private Result(T? value, Exception? ex, bool isSuccess)
    {
        Value = value;
        _exception = ex;
        IsSuccess = isSuccess;
    }

    internal static Result<T> Success(T value)
        => new(value);
    internal static Result<T> Failure(Exception exception)
        => new(exception);
}
