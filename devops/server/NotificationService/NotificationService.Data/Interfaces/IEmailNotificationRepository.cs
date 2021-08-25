// @Tai.

namespace NotificationService.Data;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NotificationService.Contracts;
using NotificationService.Contracts.Entities;

/// <summary>
/// Repository Interface for Email Notification Items.
/// </summary>
public interface IEmailNotificationRepository
{
    /// <summary>
    /// Gets the email notification items from database for the input ids.
    /// </summary>
    /// <param name="notificationIds">List of notifications ids.</param>
    /// <param name="applicationName">The Application Name (Optional).</param>
    /// <returns>List of notitication items corresponding to input ids.</returns>
    Task<IList<EmailNotificationItemEntity>> GetEmailNotificationItemEntities(IList<string> notificationIds, string applicationName = null);

    /// <summary>
    /// Gets the email notification item from database for the input id.
    /// </summary>
    /// <param name="notificationId">A single notifications id.</param>
    /// <param name="applicationName">The Application Name (Optional).</param>
    /// <returns>notitication item corresponding to input id.</returns>
    Task<EmailNotificationItemEntity> GetEmailNotificationItemEntity(string notificationId, string applicationName = null);

    /// <summary>
    /// Creates entities in database for the input email notification items.
    /// </summary>
    /// <param name="emailNotificationItemEntities">List of <see cref="EmailNotificationItemEntity"/>.</param>
    /// <param name="applicationName">The applicationName (Optional).</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    Task CreateEmailNotificationItemEntities(IList<EmailNotificationItemEntity> emailNotificationItemEntities, string applicationName = null);

    /// <summary>
    /// Saves the changes on email notification entities into database.
    /// </summary>
    /// <param name="emailNotificationItemEntities">List of <see cref="EmailNotificationItemEntity"/>.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    Task UpdateEmailNotificationItemEntities(IList<EmailNotificationItemEntity> emailNotificationItemEntities);
}