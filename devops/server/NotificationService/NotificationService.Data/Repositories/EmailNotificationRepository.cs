// @Tai.

namespace NotificationService.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NotificationService.Common;
using NotificationService.Common.Logger;
using NotificationService.Contracts;
using NotificationService.Contracts.Entities;
using NotificationService.Contracts.Extensions;
using NotificationService.Contracts.Models.Request;

/// <summary>
/// Repository for Email Notifications.
/// </summary>
public class EmailNotificationRepository : IEmailNotificationRepository
{
    /// <summary>
    /// Instance of Cosmos DB Configuration.
    /// </summary>
    private readonly CosmosDBSetting cosmosDBSetting;

    /// <summary>
    /// Instance of Application Configuration.
    /// </summary>
    private readonly ICosmosDBQueryClient cosmosDBQueryClient;

    /// <summary>
    /// Instance of <see cref="Container"/>.
    /// </summary>
    private readonly Container emailHistoryContainer;

    /// <summary>
    /// Instance of <see cref="Container"/>.
    /// </summary>
    private readonly Container meetingHistoryContainer;

    /// <summary>
    /// Instance of <see cref="ILogger"/>.
    /// </summary>
    private readonly ILogger logger;

    /// <summary>
    /// Instance of <see cref="ICosmosLinqQuery"/>.
    /// </summary>
    private readonly ICosmosLinqQuery cosmosLinqQuery;

    /// <summary>
    /// Instance of <see cref="IMailAttachmentRepository"/>.
    /// </summary>
    private readonly IMailAttachmentRepository mailAttachmentRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailNotificationRepository"/> class.
    /// </summary>
    /// <param name="cosmosDBSetting">Cosmos DB Configuration.</param>
    /// <param name="cosmosDBQueryClient">CosmosDB Query Client.</param>
    /// <param name="logger">Instance of Logger.</param>
    /// <param name="cosmosLinqQuery">Instance of Cosmos Linq query.</param>
    /// <param name="mailAttachmentRepository">Instance of the Mail Attachment repository.</param>
    public EmailNotificationRepository(IOptions<CosmosDBSetting> cosmosDBSetting, ICosmosDBQueryClient cosmosDBQueryClient, ILogger logger, ICosmosLinqQuery cosmosLinqQuery, IMailAttachmentRepository mailAttachmentRepository)
    {
        this.cosmosDBSetting = cosmosDBSetting?.Value ?? throw new System.ArgumentNullException(nameof(cosmosDBSetting));
        this.cosmosDBQueryClient = cosmosDBQueryClient ?? throw new System.ArgumentNullException(nameof(cosmosDBQueryClient));
        this.emailHistoryContainer = this.cosmosDBQueryClient.GetCosmosContainer(this.cosmosDBSetting.Database, this.cosmosDBSetting.EmailHistoryContainer);
        this.meetingHistoryContainer = this.cosmosDBQueryClient.GetCosmosContainer(this.cosmosDBSetting.Database, this.cosmosDBSetting.MeetingHistoryContainer);
        this.logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        this.cosmosLinqQuery = cosmosLinqQuery;
        this.mailAttachmentRepository = mailAttachmentRepository;
    }

    /// <inheritdoc/>
    public async Task CreateEmailNotificationItemEntities(IList<EmailNotificationItemEntity> emailNotificationItemEntities, string applicationName = null)
    {
        if (emailNotificationItemEntities is null)
        {
            throw new System.ArgumentNullException(nameof(emailNotificationItemEntities));
        }

        this.logger.TraceInformation($"Started {nameof(this.CreateEmailNotificationItemEntities)} method of {nameof(EmailNotificationRepository)}.");

        IList<EmailNotificationItemEntity> updatedEmailNotificationItemEntities = emailNotificationItemEntities;
        if (applicationName != null)
        {
            updatedEmailNotificationItemEntities = await this.mailAttachmentRepository.UploadEmail(emailNotificationItemEntities, NotificationType.Mail.ToString(), applicationName).ConfigureAwait(false);
        }

        List<Task> createTasks = new List<Task>();
        foreach (var item in updatedEmailNotificationItemEntities)
        {
            createTasks.Add(this.emailHistoryContainer.CreateItemAsync(item.ConvertToEmailNotificationItemCosmosDbEntity()));
        }

        Task.WaitAll(createTasks.ToArray());
        this.logger.TraceInformation($"Finished {nameof(this.CreateEmailNotificationItemEntities)} method of {nameof(EmailNotificationRepository)}.");

        return;
    }

    public Task CreateMeetingNotificationItemEntities(IList<MeetingNotificationItemEntity> meetingNotificationItemEntity, string applicationName)
    {
        throw new NotImplementedException();
    }

    public Task<IList<EmailNotificationItemEntity>> GetEmailNotificationItemEntities(IList<string> notificationIds, string applicationName = null)
    {
        throw new NotImplementedException();
    }

    public Task<EmailNotificationItemEntity> GetEmailNotificationItemEntity(string notificationId, string applicationName = null)
    {
        throw new NotImplementedException();
    }

    public Task<Tuple<IList<EmailNotificationItemEntity>, TableContinuationToken>> GetEmailNotifications(NotificationReportRequest notificationReportRequest)
    {
        throw new NotImplementedException();
    }

    public Task<Tuple<IList<MeetingNotificationItemEntity>, TableContinuationToken>> GetMeetingInviteNotifications(NotificationReportRequest meetingInviteReportRequest)
    {
        throw new NotImplementedException();
    }

    public Task<IList<MeetingNotificationItemEntity>> GetMeetingNotificationItemEntities(IList<string> notificationIds, string applicationName)
    {
        throw new NotImplementedException();
    }

    public Task<MeetingNotificationItemEntity> GetMeetingNotificationItemEntity(string notificationId, string applicationName)
    {
        throw new NotImplementedException();
    }

    public Task<IList<EmailNotificationItemEntity>> GetPendingOrFailedEmailNotificationsByDateRange(DateTimeRange dateRange, string applicationName, List<NotificationItemStatus> statusList, bool loadBody = false)
    {
        throw new NotImplementedException();
    }

    public Task<IList<MeetingNotificationItemEntity>> GetPendingOrFailedMeetingNotificationsByDateRange(DateTimeRange dateRange, string applicationName, List<NotificationItemStatus> statusList, bool loadBody = false)
    {
        throw new NotImplementedException();
    }

    public Task UpdateEmailNotificationItemEntities(IList<EmailNotificationItemEntity> emailNotificationItemEntities)
    {
        throw new NotImplementedException();
    }

    public Task UpdateMeetingNotificationItemEntities(IList<MeetingNotificationItemEntity> meetingNotificationItemEntity)
    {
        throw new NotImplementedException();
    }
}