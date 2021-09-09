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

    /// <summary>
    /// A constant for ResendDateRange logging.
    /// </summary>
    public const string ResendDateRange = "ResendDateRange";

    /// <summary>
    /// A constant used to insert these many items at once in a single batch to azure storage/azure storage queue
    /// The azure storage has a limitation of accepting only 100 items in a single batch, so keeping the count to 100.
    /// and storage queue message has a limitation of 64kb for message size, so restricting the count of notification ids in a single message to 100.
    /// </summary>
    public const int BatchSizeToStore = 100;
}