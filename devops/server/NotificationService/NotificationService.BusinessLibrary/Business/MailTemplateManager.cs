// @Tai.

namespace NotificationService.BusinessLibrary;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using NotificationService.Common.Logger;
using NotificationService.Contracts;
using NotificationService.Data;

/// <summary>
/// Business Manager for processing mail template.
/// </summary>
public class MailTemplateManager : IMailTemplateManager
{
    private readonly IMailTemplateRepository mailTemplateRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="MailTemplateManager"/> class.
    /// </summary>
    /// <param name="mailTemplateRepository"><see cref="IMailTemplateRepository"/> instance.</param>
    public MailTemplateManager(
        IMailTemplateRepository mailTemplateRepository)
    {
        this.mailTemplateRepository = mailTemplateRepository;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteMailTemplate(string applicationName, string templateName)
    {
        if (string.IsNullOrWhiteSpace(applicationName))
        {
            throw new ArgumentException("Application Name cannot be null or empty.", nameof(applicationName));
        }

        if (string.IsNullOrWhiteSpace(templateName))
        {
            throw new ArgumentException("Template Name cannot be null or empty.", nameof(templateName));
        }

        var traceprops = new Dictionary<string, string>();
        traceprops[AIConstants.Application] = applicationName;
        traceprops[AIConstants.MailTemplateName] = templateName;
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        bool result = false;

        //try
        //{
        //    result = await this.mailTemplateRepository.De
        //}
        return true;
    }

    public Task<MailTemplate> GetMailTemplate(string applicationName, string templateName)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SaveEmailTemplate(string applicationName, MailTemplate mailTempalte)
    {
        throw new NotImplementedException();
    }
}