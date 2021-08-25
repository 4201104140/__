// @Tai.

namespace NotificationService.UnitTests.Data.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Options;
using Moq;
using NotificationService.Common;
using NotificationService.Common.Logger;
using NotificationService.Contracts;
using NotificationService.Contracts.Entities;
using NotificationService.Contracts.Models.Request;
using NotificationService.Data;
using NotificationService.Data.Repositories;
using NUnit.Framework;

/// <summary>
/// Table Storage Repository Tests Class.
/// </summary>
public class TableStorageRepositoryTests
{
    /// <summary>
    /// Instance of Application Configuration.
    /// </summary>
    private readonly Mock<ITableStorageClient> cloudStorageClient;

    /// <summary>
    /// Application Name.
    /// </summary>
    private readonly string applicationName = "TestApp";

    /// <summary>
    /// DateRange object.
    /// </summary>
    private readonly DateTimeRange dateRange = new DateTimeRange
    {
        StartDate = DateTime.Now,
        EndDate = DateTime.Now.AddHours(2),
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="TableStorageRepositoryTests"/> class.
    /// </summary>
    public TableStorageRepositoryTests()
    {
        this.cloudStorageClient = new Mock<ITableStorageClient>();
    }


}