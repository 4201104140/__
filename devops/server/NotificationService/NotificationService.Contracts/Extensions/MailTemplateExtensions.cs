// @Tai.

namespace NotificationService.Contracts.Extensions;

using System;
using NotificationService.Common.Encryption;
using NotificationService.Contracts.Entities;

/// <summary>
/// Extensions of the <see cref="MailTemplate"/> class.
/// </summary>
public static class MailTemplateExtensions
{
    /// <summary>
    /// Converts <see cref="MailTemplateEntity"/> to a <see cref="MailTemplate"/>.
    /// </summary>
    /// <param name="mailTemplateEntity">Email template item.</param>
    /// <param name="encryptionService">Instance of encryption service to protect the secure content before saving in datastore.</param>
    /// <returns><see cref="MailTemplate"/>.</returns>
    public static MailTemplate ToContract(this MailTemplateEntity mailTemplateEntity, IEncryptionService encryptionService)
    {
        if (encryptionService is null)
        {
            throw new ArgumentNullException(nameof(encryptionService));
        }

        if (mailTemplateEntity != null)
        {
            return new MailTemplate
            {
                TemplateId = mailTemplateEntity.TemplateId,
                Description = mailTemplateEntity.Description,
                TemplateType = mailTemplateEntity.TemplateType,
                Content = encryptionService.Decrypt(mailTemplateEntity.Content),
            };
        }

        return null;
    }
}