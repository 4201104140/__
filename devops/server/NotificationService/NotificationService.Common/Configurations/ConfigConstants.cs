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
    /// A constant for RetrySetting config-section key from appsetting.json.
    /// </summary>
    public const string RetrySettingConfigSectionKey = "RetrySetting";

    /// <summary>
    /// A constant for A config section key from appsetting.json.
    /// </summary>
    public const string AIConfigSectionKey = "ApplicationInsights";

    /// <summary>
    /// The notification encryption intial vector.
    /// </summary>
    public const string NotificationEncryptionIntialVector = "NotificationEncryptionIntialVector";

    /// <summary>
    /// A constant for StorageAccount config-section key from appsetting.json.
    /// </summary>
    public const string StorageAccountConfigSectionKey = "StorageAccount";

    /// <summary>
    /// A constant for BearerTokenAuthentication config section key from appsetting.json.
    /// </summary>
    public const string BearerTokenConfigSectionKey = "BearerTokenAuthentication";

    /// <summary>
    /// A constant for StorageAccountConnectionString config key from appsetting.json.
    /// </summary>
    public const string StorageAccountConnectionStringConfigKey = "StorageAccountConnectionString";

    /// <summary>
    /// A constact for StorageAccount notification queue.
    /// </summary>
    public const string StorageAccNotificationQueueName = "NotificationQueueName";

    /// <summary>
    /// A constant for CosmosDB config section key from appsetting.json.
    /// </summary>
    public const string CosmosDBConfigSectionKey = "CosmosDB";

    /// <summary>
    /// StorageType.
    /// </summary>
    public const string StorageType = "StorageType";

    /// <summary>
    /// A constant for BearerTokenAuthenticationIssuer config key from appsetting.json.
    /// </summary>
#pragma warning disable CA2211 // Non-constant fields should not be visible
#pragma warning disable SA1401 // Fields should be private
    public static string BearerTokenIssuerConfigKey = $"{BearerTokenConfigSectionKey}:Issuer";
#pragma warning restore SA1401 // Fields should be private
#pragma warning restore CA2211 // Non-constant fields should not be visible

    /// <summary>
    /// A constant for BearerTokenAuthenticationValidAudiences config key from appsetting.json.
    /// </summary>
#pragma warning disable CA2211 // Non-constant fields should not be visible
#pragma warning disable SA1401 // Fields should be private
    public static string BearerTokenValidAudiencesConfigKey = $"{BearerTokenConfigSectionKey}:ValidAudiences";
#pragma warning restore SA1401 // Fields should be private
#pragma warning restore CA2211 // Non-constant fields should not be visible

    /// <summary>
    /// A constant for AI Tracelevel config key from appsetting.json.
    /// </summary>
#pragma warning disable CA2211 // Non-constant fields should not be visible
#pragma warning disable SA1401 // Fields should be private
    public static string AITraceLelelConfigKey = $"{AIConfigSectionKey}:TraceLevel";
#pragma warning restore SA1401 // Fields should be private
#pragma warning restore CA2211 // Non-constant fields should not be visible

    /// <summary>
    /// A constant for AI InstrumentationKey config key from appsetting.json.
    /// </summary>
#pragma warning disable CA2211 // Non-constant fields should not be visible
#pragma warning disable SA1401 // Fields should be private
    public static string AIInsrumentationConfigKey = $"{AIConfigSectionKey}:InstrumentationKey";
#pragma warning restore SA1401 // Fields should be private
#pragma warning restore CA2211 // Non-constant fields should not be visible
}