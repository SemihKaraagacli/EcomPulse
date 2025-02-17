using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BusinessLogicLayer;

public class Result<T>
{
    [JsonPropertyName("data")]
    public T? Data { get; set; }

    [JsonPropertyName("errorMessages")]
    public List<string>? ErrorMessage { get; set; }

    [JsonPropertyName("isSuccessful")]
    public bool IsSuccessful { get; set; } = true;

    [JsonPropertyName("statusCode")]
    public HttpStatusCode StatusCode { get; set; }

    [JsonConstructor]
    public Result()
    {
    }
    public Result(T data)
    {
        Data = data;
    }
    public Result(HttpStatusCode statusCode, List<string> errorMessage)
    {
        IsSuccessful = false;
        StatusCode = statusCode;
        ErrorMessage = errorMessage;
    }

    public Result(HttpStatusCode statusCode, string errorMessage)
    {
        IsSuccessful = false;
        StatusCode = statusCode;
        ErrorMessage = [errorMessage];
    }

    public static implicit operator Result<T>(T data) => new(data);


    public static implicit operator Result<T>((HttpStatusCode statusCode, string errorMessage) parameters) => new(parameters.statusCode, parameters.errorMessage);


    public static implicit operator Result<T>((HttpStatusCode statusCode, List<string> errorMessage) parameters) => new(parameters.statusCode, parameters.errorMessage);

    public static Result<T> Successful(T data) => new(data);

    public static Result<T> Failure(HttpStatusCode statusCode, List<string> errorMessage) => new(statusCode, errorMessage);

    public static Result<T> Failure(HttpStatusCode statusCode, string errorMessage) => new(statusCode, errorMessage);

    public override string ToString() => JsonSerializer.Serialize(this);

}