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

    /// <summary>
    /// This method is used to write the Custom Events. To track any events in your processing.
    /// </summary>
    /// <param name="eventName">Custom Event Name.</param>
    /// <param name="properties">custom properties, add more dimensions to this, so it will be easy to trace and query.</param>
    /// <param name="metrics">custom metrics, if you want to track any metrics of that event. For ex: duration.</param>
    /// <param name="eventCode">event code.</param>
    void WriteCustomEvent(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null, string eventCode = null);

    /// <summary>
    /// This method is used to Write Trace with Severity Level Information.
    /// </summary>
    /// <param name="message">message which should be traced. Give as detailed as you need.</param>
    /// <param name="properties">custom properties, add more dimensions to this, so it will be easy to trace and query.</param>
    void TraceInformation(string message, IDictionary<string, string> properties = null);

    /// <summary>
    /// This method is used to Write Trace with Severity Level Verbose.
    /// </summary>
    /// <param name="message">message which should be traced. Give as detailed as you need.</param>
    /// <param name="properties">custom properties, add more dimensions to this, so it will be easy to trace and query.</param>
    void TraceVerbose(string message, IDictionary<string, string> properties = null);

    /// <summary>
    /// This method is used to Write Trace with Severity Level Warning.
    /// </summary>
    /// <param name="message">message which should be traced. Give as detailed as you need.</param>
    /// <param name="properties">custom properties, add more dimensions to this, so it will be easy to trace and query.</param>
    void TraceWarning(string message, IDictionary<string, string> properties = null);
}