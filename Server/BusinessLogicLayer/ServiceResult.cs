using System.Net;

namespace BusinessLogicLayer;

public class ServiceResultEntity
{
    public List<string>? Errors { get; set; }
    public HttpStatusCode HttpStatus { get; set; }
    public bool IsSuccess => Errors is null || Errors.Count == 0;
    public bool IsError => !IsSuccess;
}


public class ServiceResult : ServiceResultEntity
{
    public static ServiceResult Success(HttpStatusCode statusCode)
        => new ServiceResult { HttpStatus = statusCode };

    public static ServiceResult Fail(List<string> errors, HttpStatusCode statusCode)
        => new ServiceResult { Errors = errors, HttpStatus = statusCode };

    public static ServiceResult Fail(string error, HttpStatusCode statusCode)
        => new ServiceResult { Errors = new List<string> { error }, HttpStatus = statusCode };
}


public class ServiceResult<T> : ServiceResult
{
    public T? Data { get; set; }

    public static ServiceResult<T> Success(T data, HttpStatusCode statusCode)
        => new ServiceResult<T> { Data = data, HttpStatus = statusCode };

    public static new ServiceResult<T> Fail(List<string> errors, HttpStatusCode statusCode)
        => new ServiceResult<T> { Errors = errors, HttpStatus = statusCode };

    public static new ServiceResult<T> Fail(string error, HttpStatusCode statusCode)
        => new ServiceResult<T> { Errors = new List<string> { error }, HttpStatus = statusCode };
}
