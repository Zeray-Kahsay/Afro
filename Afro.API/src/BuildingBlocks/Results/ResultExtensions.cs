using Afro.API.Modules.BuildingBlocks.Results;

namespace Afro.API.src.BuildingBlocks.Results;

public static class ResultExtensions
{
    public static IResult ToApiResult(this Result result)
    {
        if (result.IsSuccess)
        {
            return Microsoft.AspNetCore.Http.Results.Ok();
        }

        return Microsoft.AspNetCore.Http.Results.BadRequest(result.Error);

    }

    public static IResult ToApiResult<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            return Microsoft.AspNetCore.Http.Results.Ok(result.Value);
        }

        return Microsoft.AspNetCore.Http.Results.BadRequest(result.Error);
    }
}
