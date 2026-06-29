using Afro.API.src.BuildingBlocks.Results;

namespace Afro.API.Modules.BuildingBlocks.Results;

public class Result
{
    protected Result(bool isSuccess, Error? error = null)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public Error? Error { get; }
    public bool IsFailure => !IsSuccess;
    public static Result Success() => new(true, null);
    public static Result Failure(Error error) => new(false, error);
}
