// @Tai.

namespace NotificationService.Common.Logger;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

/// <summary>
/// Application Insights Logger helper implementation.
/// </summary>
public class AILogger : ILogger, IDisposable
{
    /// <summary>
    /// Application Insights telemetry client object to push telemetry events to AI.
    /// </summary>
    private readonly TelemetryClient telemetryClient;

    /// <summary>
    /// Configuration object to specify AI logging configurations like Severity level, component type etc.
    /// </summary>
    private readonly LoggingConfiguration loggingConfiguration;

    /// <summary>
    /// Instance of global telemetry configuration.
    /// </summary>
    private readonly TelemetryConfiguration appTelemetryConfiguration;

    private bool isDisposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="AILogger"/> class with logging configuration like Severity level, component type etc.
    /// </summary>
    /// <param name="configuration">Configuration like Severity level, component type etc.</param>
    /// <param name="telemetryConfiguration">Global telemetry configuration.</param>
    /// <param name="initializers">Array of telemetry initializers.</param>
    public AILogger(LoggingConfiguration configuration, TelemetryConfiguration telemetryConfiguration, ITelemetryInitializer[] initializers)
    {
        this.appTelemetryConfiguration = TelemetryConfiguration.CreateDefault();

        if (telemetryConfiguration != null)
        {
            this.appTelemetryConfiguration = telemetryConfiguration;
        }

        if (initializers != null && initializers.Length > 0 && this.appTelemetryConfiguration != null)
        {
            foreach (ITelemetryInitializer entry in initializers)
            {
                var initializer = this.appTelemetryConfiguration.TelemetryInitializers.FirstOrDefault(
                    x => x.GetType().Name == entry.GetType().Name);
                if (initializer != default(ITelemetryInitializer))
                {
                    _ = this.appTelemetryConfiguration.TelemetryInitializers.Remove(initializer);
                }

                this.appTelemetryConfiguration.TelemetryInitializers.Add(entry);
            }
        }

        this.telemetryClient = new TelemetryClient(this.appTelemetryConfiguration);
        this.loggingConfiguration = configuration;
        this
    }

    private void SetEnvironmentValues()
    {
        var initializer = this.appTelemetryConfiguration.TelemetryInitializers.SingleOrDefault(item => item.GetType() == typeof(Envi))
    }
}