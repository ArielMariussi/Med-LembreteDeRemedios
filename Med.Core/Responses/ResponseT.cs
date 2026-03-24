using System.Text.Json.Serialization;

namespace Med.Core.Responses;

public class Response<T> : Response
{
    public T? Data { get; init; }

    [JsonConstructor]
    public Response(bool isSuccess, string message, T? data)
    {
        IsSuccess = isSuccess;
        Message = message;
        Data = data;
    }

    public static Response<T> Ok(T data)
        => new(true,string.Empty, data );

    public static new Response<T> Fail(string message)
        => new(false, message, default);
}
