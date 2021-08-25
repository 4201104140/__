// @Tai.

namespace NotificationService.Common.Configurations;

/// <summary>
/// Configuration Constants class. 
/// </summary>
public static class ConfigConstants
{
    /// <summary>
    /// A constant for application accounts config section key.
    /// </summary>
    public const string ApplicationAccountsConfigSectionKey = "ApplicationAccounts";

    /// <summary>
    /// Seconds to wait between attempts at polling the Azure KeyVault for changes in configuration.
    /// </summary>
    public const string KeyVaultConfigRefreshDurationSeconds = "KeyVaultConfigRefreshDurationSeconds";

    /// <summary>
    /// The notification encryption key.
    /// </summary>
    public const string NotificationEncryptionKey = "NotificationEncryptionKey";

    /// <summary>
    /// The notification encryption intial vector.
    /// </summary>
    public const string NotificationEncryptionIntialVector = "NotificationEncryptionIntialVector";
}