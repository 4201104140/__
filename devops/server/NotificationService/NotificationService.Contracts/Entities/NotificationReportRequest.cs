// @Tai.

namespace NotificationService.Contracts;

using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Azure.Cosmos.Table;

/// <summary>
/// Base class for Notification Items.
/// </summary>
[DataContract]
public class NotificationReportRequest
{
    /// <summary>
    /// Gets or sets NotificationPriorityFilter.
    /// </summary>
    [DataMember(Name = "NotificationPriorityFilter")]
    public IList<NotificationPriority> NotificationPriorityFilter { get; set; }


}