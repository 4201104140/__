// @Tai.

namespace NotificationService.Common.Exceptions;

using System.Text.Json;

/// <summary>
/// Model for Error Info in response in case of exception.
/// </summary>
public class ErrorDetails
{
    /// <summary>
    /// Gets or sets the StatusCode.
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Gets or Sets Message.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Overriden method ToString.
    /// </summary>
    /// <returns>serialized ErrordDetails object. </returns>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}