using Microsoft.AspNetCore.Http;
using Utilities.Constants;

namespace Utilities.Models;
public class ResponseResult<T>
{
    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;
    public string Status { get; set; } = ResponseResultConstants.STATUS_FAILED;
    public string Message { get; set; } = string.Empty;
    public int? StatusCode { get; set; } = 200;
    public T? Data { get; set; }
    public List<string> Errors { get; set; } = new List<string>();

    internal ResponseResult(string status)
    {
        Status = status;
    }

    internal ResponseResult(string status, T data)
    {
        Status = status;
        Data = data;
    }

    internal ResponseResult(string status, string message)
    {
        Status = status;
        Message = message;
    }

    internal ResponseResult(string status, string message, T data)
    {
        Status = status;
        Message = message;
        Data = data;
    }

    internal ResponseResult(string status, string message, T data, int statusCode)
    {
        Status = status;
        Message = message;
        Data = data;
        StatusCode = statusCode;
    }

    internal ResponseResult(string status, string message, int statusCode)
    {
        Status = status;
        Message = message;
        StatusCode = statusCode;
    }

    internal ResponseResult(string status, string message, List<string> errors)
    {
        Status = status;
        Message = message;
        Errors = errors;
    }

    internal ResponseResult(string status, string message, List<string> errors, int statusCode)
    {
        Status = status;
        Message = message;
        Errors = errors;
        StatusCode = statusCode;
    }

    public ResponseResult()
    {
    }

    public static ResponseResult<T> Success()
    {
        return new ResponseResult<T>(ResponseResultConstants.STATUS_SUCCESS);
    }

    public static ResponseResult<T> Success(T data)
    {
        return new ResponseResult<T>(ResponseResultConstants.STATUS_SUCCESS, data);
    }

    public static ResponseResult<T> Success(string message)
    {
        return new ResponseResult<T>(ResponseResultConstants.STATUS_SUCCESS, message);
    }

    public static ResponseResult<T> Success(string message, T data)
    {
        return new ResponseResult<T>(ResponseResultConstants.STATUS_SUCCESS, message, data);
    }

    public static ResponseResult<T> Success(string message, T data, int statusCode)
    {
        return new ResponseResult<T>(ResponseResultConstants.STATUS_SUCCESS, message, data, statusCode);
    }

    public static ResponseResult<T> Success(string message, int statusCode)
    {
        return new ResponseResult<T>(ResponseResultConstants.STATUS_SUCCESS, message, statusCode);
    }

    public static ResponseResult<T> Failure()
    {
        return new ResponseResult<T>(ResponseResultConstants.STATUS_FAILED);
    }

    public static ResponseResult<T> Failure(string message)
    {
        return new ResponseResult<T>(ResponseResultConstants.STATUS_FAILED, message, StatusCodes.Status500InternalServerError);
    }

    public static ResponseResult<T> Failure(string message, int statusCode)
    {
        return new ResponseResult<T>(ResponseResultConstants.STATUS_FAILED, message, statusCode);
    }

    public static ResponseResult<T> Error(string message, List<string> errors)
    {
        return new ResponseResult<T>(ResponseResultConstants.STATUS_ERROR, message, errors, StatusCodes.Status500InternalServerError);
    }

    public static ResponseResult<T> Error(string message, List<string> errors, int statusCode)
    {
        return new ResponseResult<T>(ResponseResultConstants.STATUS_ERROR, message, errors, statusCode);
    }
}
