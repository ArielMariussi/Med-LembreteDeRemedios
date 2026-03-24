namespace Med.Core.Responses;

public class Response
{
    public bool IsSuccess { get;init; }
    public string? Message { get;  init; }

    public static Response Ok()
        => new() { IsSuccess = true };

    public static Response Fail(string message)
        => new() { IsSuccess = false, Message = message };
}
