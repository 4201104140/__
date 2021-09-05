// @Tai.

namespace NotificationService.Common;

/// <summary>
/// Common constants used across the solution.
/// </summary>
public static class ApplicationConstants
{
    /// <summary>
    /// Character used to split mutliple values in a string.
    /// </summary>
    public const char SplitCharacter = ';';

    /// <summary>
    /// Content Type of the body of email notifications.
    /// </summary>
    public const string EmailBodyContentType = "HTML";

    /// <summary>
    /// Name of Authorization policy to validate if Application Name is valid.
    /// </summary>
    public const string AppNameAuthorizePolicy = "AppNameAuthorize";

    /// <summary>
    /// Authentication scheme to validate the input token.
    /// </summary>
    public const string BearerAuthenticationScheme = "Bearer";

    /// <summary>
    /// EmailNotificationsFolderName.
    /// </summary>
    public const string EmailNotificationsFolderName = "EmailNotifications";
}