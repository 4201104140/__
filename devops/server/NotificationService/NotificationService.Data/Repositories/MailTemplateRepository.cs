// @Tai.

namespace NotificationService.Data.Repositories;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Options;
using NotificationService.Common;
using NotificationService.Common.Logger;
using NotificationService.Contracts.Entities;

/// <summary>
/// Repository for mail templates.
/// </summary>
public class MailTemplateRepository : IMailTemplateRepository
{
    private readonly ICloudStorageClient cloudStorageClient;
    private readonly ITableStorageClient tableStorageClient;
    private readonly CloudTable cloudTable;

    /// <summary>
    /// Initializes a new instance of the <see cref="MailTemplateRepository"/> class.
    /// </summary>
    public MailTemplateRepository(
        ICloudStorageClient cloudStorageClient,
        TableStorageClient tableStorageClient,
        IOptions<StorageAccountSetting> storageAccountSetting)
    {
        this.cloudStorageClient = cloudStorageClient ?? throw new ArgumentNullException(nameof(cloudStorageClient));
        this.tableStorageClient = tableStorageClient ?? throw new ArgumentNullException(nameof(tableStorageClient));

        if (storageAccountSetting is null)
        {
            throw new ArgumentNullException(nameof(storageAccountSetting));
        }

        if (string.IsNullOrWhiteSpace(storageAccountSetting?.Value?.MailTemplateTableName))
        {
            throw new ArgumentException("MailTemplateTableName");
        }

        this.cloudTable = this.tableStorageClient.GetCloudTable(storageAccountSetting.Value.MailTemplateTableName);
    }

    /// <inheritdoc/>
    public async Task<MailTemplateEntity> GetMailTemplate(string applicationName, string templateName)
    {
        var traceProps = new Dictionary<string, string>();
        traceProps[AIConstants.Application] = applicationName;
        traceProps[AIConstants.MailTemplateName] = templateName;

        string blobName = this.GetBlobName(applicationName, templateName);
        var contentTask = this.cloudStorageClient.DownloadBlobAsync(blobName).ConfigureAwait(false);

        TableOperation retrieveOperation = TableOperation.Retrieve<MailTemplateEntity>(applicationName, templateName);

        TableResult retrievedResult = await this.cloudTable.ExecuteAsync(retrieveOperation).ConfigureAwait(false);

        MailTemplateEntity templateEntity = retrievedResult?.Result as MailTemplateEntity;

        if (templateEntity != null)
        {
            templateEntity.Content = await contentTask;
        }

        return templateEntity;
    }

    public Task<bool> DeleteMailTemplate(string applicationName, string templateName)
    {
        throw new NotImplementedException();
    }

    public Task<IList<MailTemplateEntity>> GetAllTemplateEntities(string applicationName)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<bool> UpsertEmailTemplateEntities(MailTemplateEntity mailTemplateEntity)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Gets blob name.
    /// </summary>
    /// <param name="applicationName">Application sourcing the email template.</param>
    /// <param name="templateName">Mail template name.</param>
    /// <returns>Blob name.</returns>
    private string GetBlobName(string applicationName, string templateName)
    {
        return $"{applicationName}/EmailTemplates/{templateName}";
    }
}