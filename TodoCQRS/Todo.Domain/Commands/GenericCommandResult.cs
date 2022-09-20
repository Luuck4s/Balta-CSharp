using Todo.Shared.Commands;

namespace Todo.Domain.Commands;

public class GenericCommandResult: ICommandResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public object Data { get; set; }

    public GenericCommandResult()
    {
    }
    
    public GenericCommandResult(bool success, string message, object data)
    {
        Success = success;
        Message = message;
        Data = data;
    }
}