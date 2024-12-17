using System.Text.Json.Serialization;

namespace Dima.Core.Responses;

public class Response<TData>(TData? data, int code = Configuration.DefaultStatusCode, string? message = null)
{
    public TData? Data { get; set; } = data;
    public string? Message { get; set; } = message;

    [JsonIgnore]
    public bool IsSuccess => code is >= 200 and < 300;

    [JsonConstructor]
    public Response() : this(default(TData?), Configuration.DefaultStatusCode, null)
    {
    }
}