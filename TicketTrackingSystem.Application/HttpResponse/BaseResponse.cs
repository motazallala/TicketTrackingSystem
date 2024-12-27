using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using System.Text.Json.Serialization;

namespace TicketTrackingSystem.Application.HttpResponse;
public class BaseResponse
{
    public BaseResponse()
    {
        SuccessMessage = "The operation was completed successfully";
        Data = new object();
    }

    public bool IsSuccess { get; set; }

    [JsonIgnore]
    public dynamic? Data;

    [JsonIgnore]
    public string? SuccessMessage;

    [JsonIgnore]
    private ErrorMessage? _error;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? successMessage => IsSuccess ? SuccessMessage : null;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ErrorMessage? Error => !IsSuccess ? _error : null;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public dynamic? data => IsSuccess ? Data : null;

    public void SetError(ErrorMessage errorMessage)
    {
        _error = errorMessage;
    }

    public void SetErrorFromModelState(ModelStateDictionary modelState)
    {
        var errors = modelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();

        _error = new ErrorMessage
        {
            Code = HttpStatusCode.Conflict,
            Description = string.Join("; ", errors)
        };
    }
}