// @Tai.

namespace NotificationService.Common.Logger;

using System.Collections.Generic;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

/// <summary>
/// EnvironmentInitializer.
/// </summary>
/// <seealso cref="Microsoft.ApplicationInsights.Extensibility.ITelemetryInitializer" />
public class EnvironmentInitializer : ITelemetryInitializer
{
    /// <summary>
    /// The environment key.
    /// </summary>
    public const string EnvironmentKey = "EnvironmentName";

    /// <summary>
    /// The service line1 key.
    /// </summary>
    public const string ServiceLine1Key = "ServiceOffering";

    /// <summary>
    /// The service line2 key.
    /// </summary>
    public const string ServiceLine2Key = "ServiceLine";

    /// <summary>
    /// The service line3 key.
    /// </summary>
    public const string ServiceLine3Key = "Service";

    /// <summary>
    /// The service line4 key.
    /// </summary>
    public const string ServiceLine4Key = "ComponentName";

    /// <summary>
    /// Gets or sets plain string, value contains Level 3 hierarchy of Service Tree.
    /// </summary>
    public string Service { get; set; }

#pragma warning disable CS3001 // Argument type is not CLS-compliant
    /// <inheritdoc/>
    public void Initialize(ITelemetry telemetry)
#pragma warning restore CS3001 // Argument type is not CLS-compliant
    {
        if (telemetry?.Context == null)
        {
            return;
        }
#pragma warning disable CS0618

        var properties = telemetry.Context.Properties;

        this.ValidateEnvironementData();
    }

    // ReSharper disable once UnusedParameter.Local
    private static void ThrowExceptionIfnull(string value, string paramName)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new System.ArgumentNullException(paramName);
        }
    }

    private void ValidateEnvironementData()
    {
        ThrowExceptionIfnull(this.Service, ServiceLine3Key);
    }
}