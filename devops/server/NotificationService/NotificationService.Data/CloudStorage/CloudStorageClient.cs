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
    /// Initializes a new instance of the <see cref="CloudStorageClient"/> class.
    /// </summary>
    /// <param name="storageAccountSetting">Storage Account configuration.</param>
    public CloudStorageClient(IOptions<StorageAccountSetting> storageAccountSetting)
    {
        this.storageAccountSetting = storageAccountSetting?.Value;
        this.cloudStorageAccount = CloudStorageAccount.Parse(this.storageAccountSetting.ConnectionString);
        this.cloudQueueClient = this.cloudStorageAccount.CreateCloudQueueClient();
        this.blobContainerClient = new BlobContainerClient(this.storageAccountSetting.ConnectionString, this.storageAccountSetting.BlobContainerName);
        if (!this.blobContainerClient.Exists())
        {
            var response = this.blobContainerClient.CreateIfNotExists();

            this.blobContainerClient = new BlobContainerClient(this.storageAccountSetting.ConnectionString, this.storageAccountSetting.BlobContainerName);
        }
    }

    public Task<bool> DeleteBlobsAsync(string blobName)
    {
        throw new NotImplementedException();
    }

    public Task<string> DownloadBlobAsync(string blobName)
    {
        throw new NotImplementedException();
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

    public Task<string> UploadBlobAsync(string blobName, string content)
    {
        throw new NotImplementedException();
    }
}