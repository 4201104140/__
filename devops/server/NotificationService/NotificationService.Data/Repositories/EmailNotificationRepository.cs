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
    /// Initializes a new instance of the <see cref="EmailNotificationRepository"/> class.
    /// </summary>
    /// <param name="cosmosDBSetting">Cosmos DB Configuration.</param>
    /// <param name="cosmosDBQueryClient">CosmosDB Query Client.</param>
    public EmailNotificationRepository(IOptions<CosmosDBSetting> cosmosDBSetting, ICosmosDBQueryClient cosmosDBQueryClient)
    {
        this.cosmosDBSetting = cosmosDBSetting?.Value ?? throw new System.ArgumentNullException(nameof(cosmosDBSetting));
        this.cosmosDBQueryClient = cosmosDBQueryClient ?? throw new System.ArgumentNullException(nameof(cosmosDBQueryClient));

        this.emailHistoryContainer = this.cosmosDBQueryClient.GetCosmosContainer(this.cosmosDBSetting.Database, this.cosmosDBSetting.EmailHistoryContainer);
    }

    /// <inheritdoc/>
    public async Task CreateEmailNotificationItemEntities(IList<EmailNotificationItemEntity> emailNotificationItemEntities, string applicationName = null)
    {
        if (emailNotificationItemEntities is null)
        {
            throw new System.ArgumentNullException(nameof(emailNotificationItemEntities));
        }

        IList<EmailNotificationItemEntity> updatedEmailNotificationItemEntities = emailNotificationItemEntities;
        List<Task> createTasks = new List<Task>();
        foreach (var item in updatedEmailNotificationItemEntities)
        {
            createTasks.Add(this.emailHistoryContainer.CreateItemAsync(item.ConvertToEmailNotificationItemCosmosDbEntity()));
        }

        Task.WaitAll(createTasks.ToArray());

        return;
    }

    public Task<IList<EmailNotificationItemEntity>> GetEmailNotificationItemEntities(IList<string> notificationIds, string applicationName = null)
    {
        throw new NotImplementedException();
    }

    public Task<EmailNotificationItemEntity> GetEmailNotificationItemEntity(string notificationId, string applicationName = null)
    {
        throw new NotImplementedException();
    }

    public Task UpdateEmailNotificationItemEntities(IList<EmailNotificationItemEntity> emailNotificationItemEntities)
    {
        throw new NotImplementedException();
    }
}