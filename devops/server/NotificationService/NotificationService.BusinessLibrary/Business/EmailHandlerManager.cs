// @Tai.

namespace NotificationService.BusinessLibrary.Business;

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NotificationService.BusinessLibrary.Interfaces;
using NotificationService.Common;
using NotificationService.Common.Configurations;
using NotificationService.Common.Logger;
using NotificationService.Contracts;
using NotificationService.Contracts.Entities;
using NotificationService.Contracts.Models;
using NotificationService.Contracts.Models.Request;
using NotificationService.Data;

/// <summary>
/// Business Manager for Notification Handler.
/// </summary>
public class EmailHandlerManager : IEmailHandlerManager
{
    /// <summary>
    /// MS Graph configuration.
    /// </summary>
    private readonly MSGraphSetting mSGraphSetting;

    /// <summary>
    /// Instance of Application Configuration.
    /// </summary>
    private readonly IConfiguration configuration;

    /// <summary>
    /// Instance of <see cref="ICloudStorageClient"/>.
    /// </summary>
    private readonly ICloudStorageClient cloudStorageClient;

    /// <summary>
    /// Instance of <see cref="ILogger"/>.
    /// </summary>
    private readonly ILogger logger;

    /// <summary>
    /// Instance of <see cref="IEmailManager"/>.
    /// </summary>
    private readonly IEmailManager emailManager;

    /// <summary>
    /// StorageAccountSetting configuration object.
    /// </summary>
    private readonly string notificationQueue;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailHandlerManager"/> class.
    /// </summary>
    /// <param name="configuration">An instance of <see cref="IConfiguration"/>.</param>
    /// <param name="mSGraphSetting">Graph settings  <see cref="MSGraphSetting"/>.</param>
    /// <param name="cloudStorageClient">An instance of <see cref="ICloudStorageClient"/>.</param>
    /// <param name="logger">An instance of <see cref="ILogger"/>.</param>
    /// <param name="emailManager">An instance of <see cref="IEmailManager"/>.</param>
    public EmailHandlerManager(
        IConfiguration configuration,
        IOptions<MSGraphSetting> mSGraphSetting,
        ICloudStorageClient cloudStorageClient,
        ILogger logger,
        IEmailManager emailManager)
    {
        this.configuration = configuration;
        this.mSGraphSetting = mSGraphSetting?.Value;
        this.cloudStorageClient = cloudStorageClient;
        this.logger = logger;
        this.emailManager = emailManager;
        this.notificationQueue = this.configuration?[$"{ConfigConstants.StorageAccountConfigSectionKey}:{ConfigConstants.StorageAccNotificationQueueName}"];
    }

    /// <inheritdoc/>
    public async Task<IList<NotificationResponse>> QueueEmailNotifications(string applicationName, EmailNotificationItem[] emailNotificationItems)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var traceProps = new Dictionary<string, string>();
        bool result = false;
        try
        {
            this.logger.TraceInformation($"Started {nameof(this.QueueEmailNotifications)} method of {nameof(EmailHandlerManager)}.", traceProps);
            if (string.IsNullOrWhiteSpace(applicationName))
            {
                throw new ArgumentException("Application Name cannot be null or empty.", nameof(applicationName));
            }

            if (emailNotificationItems is null)
            {
                throw new ArgumentNullException(nameof(emailNotificationItems));
            }

            traceProps[AIConstants.Application] = applicationName;
            traceProps[AIConstants.EmailNotificationCount] = emailNotificationItems.Length.ToString(CultureInfo.InvariantCulture);

            this.logger.WriteCustomEvent("QueueEmailNotification Started", traceProps);
            IList<NotificationResponse> notificationResponses = new List<NotificationResponse>();
            
        }
        catch (Exception e)
        {
            this.logger.WriteException(e, traceProps);
            result = false;
            throw;
        }
        finally
        {
            stopwatch.Stop();
            traceProps[AIConstants.Result] = result.ToString(CultureInfo.InvariantCulture);
            var metrics = new Dictionary<string, double>();
            metrics[AIConstants.Duration] = stopwatch.ElapsedMilliseconds;
            this.logger.WriteCustomEvent("QueueEmailNotifications Completed", traceProps, metrics);
        }
    }

    public Task<IList<NotificationResponse>> ResendNotifications(string applicationName, string[] notificationIds, NotificationType notifType = NotificationType.Mail, bool ignoreAlreadySent = false)
    {
        throw new NotImplementedException();
    }

    public Task<IList<NotificationResponse>> ResendEmailNotificationsByDateRange(string applicationName, DateTimeRange dateRange)
    {
        throw new NotImplementedException();
    }

    public Task<IList<NotificationResponse>> ResendMeetingNotificationsByDateRange(string applicationName, DateTimeRange dateRange)
    {
        throw new NotImplementedException();
    }
}