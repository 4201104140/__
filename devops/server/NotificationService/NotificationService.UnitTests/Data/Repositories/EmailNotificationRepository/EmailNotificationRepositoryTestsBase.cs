// @Tai.

namespace NotificationService.UnitTests.Data.Repositories;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using Moq;
using NotificationService.Common;
using NotificationService.Common.Logger;
using NotificationService.Contracts;
using NotificationService.Contracts.Entities;
using NotificationService.Contracts.Extensions;
using NotificationService.Data;

/// <summary>
/// Base class for Email Notification Repository class tests.
/// </summary>
[ExcludeFromCodeCoverage]
public class EmailNotificationRepositoryTestsBase
{
    /// <summary>
    /// Gets or sets CosmosDBSetting Configuration Mock.
    /// </summary>
    public IOptions<CosmosDBSetting> CosmosDBSetting { get; set; }

    /// <summary>
    /// Gets or sets Cosmos DB Query Client Mock.
    /// </summary>
    public Mock<ICosmosDBQueryClient> CosmosDBQueryClient { get; set; }

    /// <summary>
    /// Gets or sets Cosmos Container Mock.
    /// </summary>
    public Mock<Container> EmailHistoryContainer { get; set; }

    /// <summary>
    /// Gets or sets Cosmos Container Mock.
    /// </summary>
    public Mock<Container> MeetingHistoryContainer { get; set; }

    /// <summary>
    /// Gets or sets Email Notification Repository instance.
    /// </summary>
    public EmailNotificationRepository EmailNotificationRepository { get; set; }

    /// <summary>
    /// Gets Test Application name.
    /// </summary>
    public string ApplicationName
    {
        get => "TestApp";
    }

    /// <summary>
    /// Gets test notification entities.
    /// </summary>
    public IList<EmailNotificationItemEntity> NotificationEntities
    {
        get => new List<EmailNotificationItemEntity>()
            {
                new EmailNotificationItemEntity()
                {
                    Application = this.ApplicationName,
                    Id = "TestId",
                    To = "user@contoso.com",
                    Subject = "TestSubject",
                    Body = "TestBody",
                },
                new EmailNotificationItemEntity()
                {
                    Application = this.ApplicationName,
                    Id = "TestId2",
                    To = "user@contoso.com",
                    Subject = "TestSubject",
                    Body = "TestBody",
                },
            };
    }

    /// <summary>
    /// Gets MailHistoryContainerName.
    /// </summary>
    protected string MailHistoryContainerName { get => "TestEmailContainer"; }

    /// <summary>
    /// Initialization for all Email Manager Tests.
    /// </summary>
    protected void SetupTestBase()
    {
        this.CosmosDBQueryClient = new Mock<ICosmosDBQueryClient>();
        this.EmailHistoryContainer = new Mock<Container>();

        this.CosmosDBSetting = Options.Create(new CosmosDBSetting() { Database = "TestDatabase", EmailHistoryContainer = this.MailHistoryContainerName, MeetingHistoryContainer = "", Key = "6VsjYPZWq5PiDumEf1YMdzwMJXhGt3U9rBYcWwMSXDrCAx05d7Frq9FADRZHnXsO1p4e2eINvGTtN7WBiq6S0A==", Uri = "https://phantai.table.cosmos.azure.com:443/" });

        _ = this.CosmosDBQueryClient
                .Setup(cdq => cdq.GetCosmosContainer(It.IsAny<string>(), this.MailHistoryContainerName))
                .Returns(this.EmailHistoryContainer.Object);

        this.EmailNotificationRepository = new EmailNotificationRepository(this.CosmosDBSetting, this.CosmosDBQueryClient.Object);
    }
}