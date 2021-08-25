﻿// @Tai.

namespace NotificationService.Common;

/// <summary>
/// Storage Account Configuration Settings.
/// </summary>
public class StorageAccountSetting
{
    /// <summary>
    /// Gets or sets Connection string.
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    /// Gets or sets the Blob Container Name.
    /// </summary>
    public string BlobContainerName { get; set; }

    /// <summary>
    /// Gets or sets the Mail template table name.
    /// </summary>
    public string MailTemplateTableName { get; set; }
}
