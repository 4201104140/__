using NotificationService.Contracts;
using NotificationService.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Data.Repositories;

using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NotificationService.Common;
using NotificationService.Common.Encryption;
using NotificationService.Common.Logger;
using NotificationService.Contracts;
using NotificationService.Contracts.Entities;

/// <summary>
/// The Mail Attachment Repositoy class.
/// </summary>
public class MailAttachmentRepository : IMailAttachmentRepository
{
    /// <summary>
    /// Instance of <see cref="ILogger"/>.
    /// </summary>
    private readonly ILogger logger;

    /// <summary>
    /// Instance of <see cref="ICloudStorageClient"/>.
    /// </summary>
    private readonly ICloudStorageClient cloudStorageClient;

    /// <summary>
    /// Instance of <see cref="IEncryptionService"/>.
    /// </summary>
    private readonly IEncryptionService encryptionService;

    /// <summary>
    /// Initializes a new instance of the <see cref="MailAttachmentRepository"/> class.
    /// </summary>
    /// <param name="logger">The Logger instance.</param>
    /// <param name="cloudStorageClient">The Cloud Storage Client instance.</param>
    /// <param name="encryptionService">The IEncryptionService instance.</param>
    public MailAttachmentRepository(ILogger logger, ICloudStorageClient cloudStorageClient, IEncryptionService encryptionService)
    {
        this.logger = logger;
        this.cloudStorageClient = cloudStorageClient;
        this.encryptionService = encryptionService;
    }

    /// <inheritdoc/>
    public async Task<IList<EmailNotificationItemEntity>> UploadEmail(IList<EmailNotificationItemEntity> emailNotificationItemEntities, string notificationType, string applicationName)
    {
        var traceProps = new Dictionary<string, string>();
        traceProps[AIConstants.Application] = applicationName;
        traceProps[AIConstants.NotificationType] = notificationType;
        traceProps[AIConstants.EmailNotificationCount] = emailNotificationItemEntities?.Count.ToString(CultureInfo.InvariantCulture);

        this.logger.TraceInformation($"Started {nameof(this.UploadEmail)} method of {nameof(MailAttachmentRepository)}.", traceProps);
        IList<EmailNotificationItemEntity> notificationEntities = new List<EmailNotificationItemEntity>();
        if (!(emailNotificationItemEntities is null) && emailNotificationItemEntities.Count > 0)
        {
            foreach (var item in emailNotificationItemEntities)
            {
                var notificationEntity = item;
                var blobEmailData = new BlobEmailData
                {
                    NotificationId = item.NotificationId,
                    Body = item.Body,
                    Attachments = item.Attachments,
                    TemplateData = item.TemplateData,
                };
                var blobpath = this.GetBlobPath(applicationName, item.NotificationId, ApplicationConstants.EmailNotificationsFolderName);
                var uloadedblobpath = await this.cloudStorageClient.UploadBlobAsync(blobpath, this.encryptionService.Encrypt(JsonConvert.SerializeObject(blobEmailData))).ConfigureAwait(false);
                notificationEntities.Add(notificationEntity);
            }
        }

        this.logger.TraceInformation($"Finished {nameof(this.UploadEmail)} method of {nameof(MailAttachmentRepository)}.", traceProps);
        return notificationEntities;
    }

    public Task<IList<EmailNotificationItemEntity>> DownloadEmail(IList<EmailNotificationItemEntity> emailNotificationItemEntities, string applicationName)
    {
        throw new NotImplementedException();
    }

    public Task<IList<MeetingNotificationItemEntity>> DownloadMeetingInvite(IList<MeetingNotificationItemEntity> meetinglNotificationItemEntities, string applicationName)
    {
        throw new NotImplementedException();
    }

    public Task<IList<MeetingNotificationItemEntity>> UploadMeetingInvite(IList<MeetingNotificationItemEntity> meetingNotificationItemEntities, string applicationName)
    {
        throw new NotImplementedException();
    }

    private string GetBlobPath(string applicationName, string notificationId, string folderName)
    {
        return $"{applicationName}/{folderName}/{notificationId}";
    }
}