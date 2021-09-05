// @Tai.

namespace NotificationService.Data;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using Microsoft.Extensions.Options;
using NotificationService.Common;
using NotificationService.Common.Logger;

/// <summary>
/// Client Interface to the Azure Cloud Storage.
/// </summary>
public class CloudStorageClient : ICloudStorageClient
{
    /// <summary>
    /// Instance of <see cref="StorageAccountSetting"/>.
    /// </summary>
    private readonly StorageAccountSetting storageAccountSetting;

    /// <summary>
    /// Instance of <see cref="CloudStorageAccount"/>.
    /// </summary>
    private readonly CloudStorageAccount cloudStorageAccount;

    /// <summary>
    /// Instance of <see cref="CloudQueueClient"/>.
    /// </summary>
    private readonly CloudQueueClient cloudQueueClient;

    /// <summary>
    /// Instance of <see cref="BlobContainerClient"/>.
    /// </summary>
    private readonly BlobContainerClient blobContainerClient;

    /// <summary>
    /// Instance of <see cref="ILogger"/>.
    /// </summary>
    private readonly ILogger logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CloudStorageClient"/> class.
    /// </summary>
    /// <param name="storageAccountSetting">Storage Account configuration.</param>
    /// <param name="logger"><see cref="ILogger"/> instance.</param>
    public CloudStorageClient(IOptions<StorageAccountSetting> storageAccountSetting, ILogger logger)
    {
        this.storageAccountSetting = storageAccountSetting?.Value;
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.cloudStorageAccount = CloudStorageAccount.Parse(this.storageAccountSetting.ConnectionString);
        this.cloudQueueClient = this.cloudStorageAccount.CreateCloudQueueClient();
        this.blobContainerClient = new BlobContainerClient(this.storageAccountSetting.ConnectionString, this.storageAccountSetting.BlobContainerName);
        if (!this.blobContainerClient.Exists())
        {
            this.logger.TraceWarning($"BlobStorageClient - Method: {nameof(CloudStorageClient)} - No container found with name {this.storageAccountSetting.BlobContainerName}.");

            var response = this.blobContainerClient.CreateIfNotExists();

            this.blobContainerClient = new BlobContainerClient(this.storageAccountSetting.ConnectionString, this.storageAccountSetting.BlobContainerName);
        }
    }

    /// <inheritdoc/>
    public CloudQueue GetCloudQueue(string queueName)
    {
        CloudQueue cloudQueue = this.cloudQueueClient.GetQueueReference(queueName);
        _ = cloudQueue.CreateIfNotExists();
        return cloudQueue;
    }

    /// <inheritdoc/>
    public Task QueueCloudMessages(CloudQueue cloudQueue, IEnumerable<string> messages, TimeSpan? initialVisibilityDelay = null)
    {
        messages.ToList().ForEach(msg =>
        {
            CloudQueueMessage message = new CloudQueueMessage(msg);
            cloudQueue.AddMessage(message, null, initialVisibilityDelay);
        });
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public async Task<string> UploadBlobAsync(string blobName, string content)
    {
        BlobClient blobClient = this.blobContainerClient.GetBlobClient(blobName);
        var contentBytes = Convert.FromBase64String(content);
        using (var stream = new MemoryStream(contentBytes))
        {
            var result = await blobClient.UploadAsync(stream, overwrite: true).ConfigureAwait(false);
        }

        return string.Concat(this.blobContainerClient.Uri, "/", blobName);
    }

    public Task<bool> DeleteBlobsAsync(string blobName)
    {
        throw new NotImplementedException();
    }

    public Task<string> DownloadBlobAsync(string blobName)
    {
        throw new NotImplementedException();
    }
}