// @Tai.

namespace NotificationService.Common.Logger;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Extensions.Logging;

/// <summary>
/// ILogger interface with the declaration of methods for logging.
/// </summary>
public interface ILogger
{
    /// <summary>
    /// This method is used to log exceptions along with the parameter values.
    /// </summary>
    /// <param name="exception">exception object.</param>
    /// <param name="properties">custom properties of the exception.</param>
    /// <param name="metrics">custom metrics of the exception.</param>
    /// <param name="eventCode">Any event code for exception.</param>
    void WriteException(Exception exception, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null, string eventCode = null);
}