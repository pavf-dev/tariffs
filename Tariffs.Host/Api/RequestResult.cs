using System.Text.Json.Serialization;

namespace Tariffs.Host.Api;

public class RequestResult<T>
{
    public RequestResult(T data)
    {
        Data = data;
    }

    public RequestResult(IEnumerable<Error> errors)
    {
        Errors = errors;
    }
    
    [JsonPropertyName("data")]
    public T Data { get; }
    
    [JsonPropertyName("errors")]
    public IEnumerable<Error> Errors { get; }
}

public class Error
{
    public Error(string message)
    {
        Message = message;
    }

    public Error(string message, int errorCode)
    {
        Message = message;
        ErrorCode = errorCode;
    }
    
    [JsonPropertyName("message")]
    public string Message { get; }
    
    [JsonPropertyName("errorCode")]
    public int ErrorCode { get; }
}