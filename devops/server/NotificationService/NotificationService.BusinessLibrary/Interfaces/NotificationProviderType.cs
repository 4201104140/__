// @Tai.

namespace NotificationService.BusinessLibrary.Interfaces;

/// <summary>
/// NotificationProviderType.
/// </summary>
public enum NotificationProviderType
{
    /// <summary>
    /// Graph API
    /// </summary>
    Graph,

    /// <summary>
    /// Direct Send API
    /// </summary>
    DirectSend,

    /// <summary>
    /// SMTP API
    /// </summary>
    SMTP,
}