// @Tai.

namespace NotificationHandler.Controllers;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NotificationService.BusinessLibrary;
using NotificationService.BusinessLibrary.Interfaces;
using NotificationService.Common;
using NotificationService.Common.Logger;
using NotificationService.Contracts;
using NotificationService.Contracts.Models.Request;

/// <summary>
/// Controller to handle email notifications.
/// </summary>
[ApiController]
[Route("v1/email")]
public class EmailController : BaseController
{
    private readonly IMailTemplateManager templateManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailController"/> class.
    /// </summary>
    /// <param name="templateManager">An instance of <see cref="MailTemplateManager"/>.</param>
    /// <param name="logger">An instance of <see cref="ILogger"/>.</param>
    public EmailController(IMailTemplateManager templateManager, ILogger logger)
        : base(logger)
    {
        this.templateManager = templateManager;
    }

    /// <summary>
    /// Gets the mail template.
    /// </summary>
    /// <param name="applicationName">Application sourcing the email notification.</param>
    /// <param name="mailTemplateName">Template name.</param>
    /// <returns><see cref="MailTemplate"/>.</returns>
    [HttpGet("mailTemplate/{applicationName}/{mailTemplateName}")]
    public async Task<IActionResult> GetMailTemplate(string applicationName, string mailTemplateName)
    {
        try
        {
            var traceProps = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(applicationName))
            {
                this.LogAndThrowArgumentNullException("Application Name cannot be null or empty.", nameof(applicationName), traceProps);
            }

            this.LogAndThrowArgumentNullException("Application Name cannot be null or empty.", nameof(applicationName), traceProps);

            return Ok();
        }
        catch
        {
            return Ok();
        }
    }

    /// <summary>
    /// Saves mail template.
    /// </summary>
    /// <param name="applicationName">Application sourcing the email notification.</param>
    /// <param name="mailTemplate"><see cref="MailTemplate"/>.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpPost("mailTemplate/{applicationName}")]
    public async Task<IActionResult> SaveMailTemplate(string applicationName, [FromBody] MailTemplate mailTemplate)
    {
        try
        {
            var traceProps = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(applicationName))
            {
                this.LogAndThrowArgumentNullException("Application Name cannot be null or empty.", nameof(applicationName), traceProps);
            }

            if (mailTemplate is null)
            {
                this.LogAndThrowArgumentNullException("Mail template param should not be null", nameof(mailTemplate), traceProps);
            }

            if (string.IsNullOrWhiteSpace(mailTemplate?.TemplateId))
            {
                this.LogAndThrowArgumentNullException("Template name should not be empty", nameof(mailTemplate), traceProps);
            }

            if (string.IsNullOrWhiteSpace(mailTemplate.Description))
            {
                this.LogAndThrowArgumentNullException("Template description should not be empty", nameof(mailTemplate), traceProps);
            }

            if (string.IsNullOrWhiteSpace(mailTemplate.Content))
            {
                this.LogAndThrowArgumentNullException("Template content should not be empty", nameof(mailTemplate), traceProps);
            }

            if (string.IsNullOrWhiteSpace(mailTemplate.TemplateType))
            {
                this.LogAndThrowArgumentNullException("Template type should not be empty", nameof(mailTemplate), traceProps);
            }

            if (!(mailTemplate.TemplateType.ToLowerInvariant() == "xslt" || mailTemplate.TemplateType.ToLowerInvariant() == "text"))
            {
                this.LogAndThrowArgumentNullException("Template type should be 'Text' or 'XSLT'", nameof(mailTemplate), traceProps);
            }

            traceProps[AIConstants.Application] = applicationName;
            bool result;
            this.logger.TraceInformation($"Started {nameof(this.SaveMailTemplate)} method of {nameof(EmailController)}.", traceProps);
            result = await this.templateManager.SaveEmailTemplate(applicationName, mailTemplate).ConfigureAwait(false);
            this.logger.TraceInformation($"Finished {nameof(this.SaveMailTemplate)} method of {nameof(EmailController)}.", traceProps);
            return this.Accepted(result);
        }
        catch (ArgumentNullException agNullEx)
        {
            return this.BadRequest(agNullEx.Message);
        }
        catch (ArgumentException agEx)
        {
            return this.BadRequest(agEx.Message);
        }
        catch (Exception ex)
        {
            this.logger.WriteException(ex);
            throw;
        }
    }
}
