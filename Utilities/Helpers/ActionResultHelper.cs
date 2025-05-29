using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utilities.Models;

namespace Utilities.Helpers;

public class ActionResultHelper
{
    public static ActionResult<ResponseResult<T>> MapResponseByStatusCode<T>(ResponseResult<T> result)
    {
        if (null == result)
            throw new ArgumentNullException(nameof(result));

        return result.StatusCode switch
        {
            StatusCodes.Status200OK => new ObjectResult(result)
            {
                StatusCode = StatusCodes.Status200OK
            },
            StatusCodes.Status201Created => new ObjectResult(result)
            {
                StatusCode = StatusCodes.Status201Created
            },
            StatusCodes.Status400BadRequest => new ObjectResult(result)
            {
                StatusCode = StatusCodes.Status400BadRequest
            },
            StatusCodes.Status500InternalServerError => new ObjectResult(result)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            },
            _ => new ObjectResult(result)
            {
                StatusCode = result.StatusCode,
            }
        };
    }
}
