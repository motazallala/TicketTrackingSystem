namespace TicketTrackingSystem.Application.Model;
public class Result<T>
{
    public bool IsSuccess { get; private set; }
    public T Value { get; private set; }
    public string ErrorMessage { get; private set; }

    private Result(bool isSuccess, T value, string errorMessage)
    {
        IsSuccess = isSuccess;
        Value = value;
        ErrorMessage = errorMessage;
    }
    private Result(bool isSuccess, string errorMessage)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }

    public static Result<T> Success() => new Result<T>(true, null);
    public static Result<T> Success(T value) => new Result<T>(true, value, null);
    public static Result<T> Failure(string errorMessage) => new Result<T>(false, default, errorMessage);
}
