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
        var errors = modelState
            .Where(kvp => kvp.Value.Errors != null)
            .SelectMany(kvp => kvp.Value.Errors.Select(e => new { Field = kvp.Key.Split('.').Last(), Error = e.ErrorMessage }))
            .ToList();

        // Group errors by field and join error messages for each field
        var errorDictionary = errors
            .GroupBy(e => e.Field)
            .ToDictionary(g => g.Key, g => string.Join(", ", g.Select(e => e.Error)));

        // Format errors as 'Field: ErrorMessage' and join with a new line for each error
        var description = string.Join(", ", errorDictionary.Select(e => $"{e.Key} : {e.Value}"));

        _error = new ErrorMessage
        {
            Code = HttpStatusCode.Conflict,
            Description = description
        };
    }

    public void SetErrorFromModelStateAsDictionary(ModelStateDictionary modelState, string descriptionMessage = "The Data You Send Not Correct!")
    {
        var errors = modelState
            .Where(kvp => kvp.Value.Errors != null)
            .SelectMany(kvp => kvp.Value.Errors.Select(e => new { Field = kvp.Key.Split('.').Last(), Error = e.ErrorMessage }))
            .ToList();

        // Group errors by field and join error messages for each field
        var errorDictionary = errors
            .GroupBy(e => e.Field)
            .ToDictionary(g => g.Key, g => string.Join(", ", g.Select(e => e.Error)));

        _error = new ErrorMessage
        {
            Code = HttpStatusCode.Conflict,
            Description = descriptionMessage,
            Validation = errorDictionary
        };
    }
}
