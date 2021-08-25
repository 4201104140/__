// @Tai.

namespace NotificationService.Contracts;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using NotificationService.Common.Configurations;
using NotificationService.Contracts.Entities;

/// <summary>
/// Extensions of the <see cref="EmailNotificationItemEntity"/> class.
/// </summary>
[ExcludeFromCodeCoverage]
public static class EmailNotificationItemEntityExtensions
{
    /// <summary>
    /// Converts <see cref="EmailNotificationItemEntity"/> to a <see cref="EmailNotificationItemCosmosDbEntity"/>.
    /// </summary>
    /// <param name="emailNotificationItemEntity">Email Notification Item Entity.</param>
    /// <returns><see cref="EmailNotificationItemCosmosDbEntity"/>.</returns>
    public static EmailNotificationItemCosmosDbEntity ConvertToEmailNotificationItemCosmosDbEntity(this EmailNotificationItemEntity emailNotificationItemEntity)
    {
        if (emailNotificationItemEntity is null)
        {
            return null;
        }

        EmailNotificationItemCosmosDbEntity emailNotificationItemTableEntity = new EmailNotificationItemCosmosDbEntity();
        emailNotificationItemTableEntity.PartitionKey = emailNotificationItemEntity.Application;
        emailNotificationItemTableEntity.RowKey = emailNotificationItemEntity.NotificationId;
        emailNotificationItemTableEntity.Id = emailNotificationItemEntity.Id;
        emailNotificationItemTableEntity.Application = emailNotificationItemEntity.Application;
        emailNotificationItemTableEntity.BCC = emailNotificationItemEntity.BCC;
        emailNotificationItemTableEntity.CC = emailNotificationItemEntity.CC;
        emailNotificationItemTableEntity.EmailAccountUsed = emailNotificationItemEntity.EmailAccountUsed;
        emailNotificationItemTableEntity.ErrorMessage = emailNotificationItemEntity.ErrorMessage;
        emailNotificationItemTableEntity.From = emailNotificationItemEntity.From;
        emailNotificationItemTableEntity.NotificationId = emailNotificationItemEntity.NotificationId;
        emailNotificationItemTableEntity.Priority = emailNotificationItemEntity.Priority.ToString();
        emailNotificationItemTableEntity.ReplyTo = emailNotificationItemEntity.ReplyTo;
        emailNotificationItemTableEntity.Sensitivity = emailNotificationItemEntity.Sensitivity;
        emailNotificationItemTableEntity.Status = emailNotificationItemEntity.Status.ToString();
        emailNotificationItemTableEntity.Subject = emailNotificationItemEntity.Subject;
        emailNotificationItemTableEntity.TemplateId = emailNotificationItemEntity.TemplateId;
        emailNotificationItemTableEntity.Timestamp = emailNotificationItemEntity.Timestamp;
        emailNotificationItemTableEntity.To = emailNotificationItemEntity.To;
        emailNotificationItemTableEntity.TrackingId = emailNotificationItemEntity.TrackingId;
        emailNotificationItemTableEntity.TryCount = emailNotificationItemEntity.TryCount;
        emailNotificationItemTableEntity.ETag = emailNotificationItemEntity.ETag;
        emailNotificationItemTableEntity.SendOnUtcDate = emailNotificationItemEntity.SendOnUtcDate;
        return emailNotificationItemTableEntity;
    }
}