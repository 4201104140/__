// @Tai.

namespace NotificationHandler.Controllers;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Contracts;

/// <summary>
/// Controller to handle email notifications.
/// </summary>
[ApiController]
[Route("v1/email")]
public class EmailController : ControllerBase
{
    
    public async Task<IActionResult> SaveMailTemplate(string applicationName, [FromBody] MailTemplate mailTemplate)
    {
        try
        {
            var traceProps = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(applicationName))
            {
                throw new ArgumentNullException(nameof(applicationName));
            }

            if (mailTemplate is null)
            {
                throw new ArgumentNullException(nameof(mailTemplate));
            }

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }
}
