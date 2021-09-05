// @Tai.

namespace NotificationService.Contracts.Extensions;

using System.Collections.Generic;
using System.Linq;
using NotificationService.Contracts.Entities;
using NotificationService.Contracts.Models;

/// <summary>
/// MeetingNotificationItemExtensions.
/// </summary>
public static class MeetingNotificationItemExtensions
{
    /// <summary>
    /// Converts to entity.
    /// </summary>
    /// <param name="meetingNotificationItem">The meeting notification item.</param>
    /// <param name="applicationName">Name of the application.</param>
    /// <returns>A <see cref="MeetingNotificationItemEntity"/>.</returns>
    public static MeetingNotificationItemEntity ToEntity(this MeetingNotificationItem meetingNotificationItem, string applicationName)
    {
        if (meetingNotificationItem != null)
        {
            return new MeetingNotificationItemEntity
            {
                Attachments = ToNotificationAttachmentEntities(meetingNotificationItem.Attachments),
                ICalUid = meetingNotificationItem.ICalUid,
                OptionalAttendees = meetingNotificationItem.OptionalAttendees,
                RequiredAttendees = meetingNotificationItem.RequiredAttendees,
                Body = meetingNotificationItem?.Body,
                NotificationId = meetingNotificationItem.NotificationId,
                DayofMonth = meetingNotificationItem.DayofMonth,
                DayOfWeekByMonth = meetingNotificationItem.DayOfWeekByMonth,
                DaysOfWeek = meetingNotificationItem.DaysOfWeek,
                From = meetingNotificationItem.From,
                EndDate = meetingNotificationItem.EndDate,
                End = meetingNotificationItem.End,
                Interval = meetingNotificationItem.Interval,
                IsOnlineMeeting = meetingNotificationItem.IsOnlineMeeting,
                IsResponseRequested = meetingNotificationItem.IsResponseRequested,
                IsPrivate = meetingNotificationItem.IsPrivate,
                OccurrenceId = meetingNotificationItem.OccurrenceId,
                SequenceNumber = meetingNotificationItem.SequenceNumber,
                Start = meetingNotificationItem.Start,
                Subject = meetingNotificationItem.Subject,
                ReminderMinutesBeforeStart = meetingNotificationItem.ReminderMinutesBeforeStart,
                IsAllDayEvent = meetingNotificationItem.IsAllDayEvent,
                IsCancel = meetingNotificationItem.IsCancel,
                Location = meetingNotificationItem.Location,
                MonthOfYear = meetingNotificationItem.MonthOfYear,
                Ocurrences = meetingNotificationItem.Ocurrences,
                Priority = meetingNotificationItem.Priority,
                RecurrencePattern = meetingNotificationItem.RecurrencePattern,
                TemplateId = meetingNotificationItem.TemplateId,
                TemplateData = meetingNotificationItem.TemplateData,
                Application = applicationName,
                TrackingId = meetingNotificationItem.TrackingId,
                Action = meetingNotificationItem.Action,
            };
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    ///  Convert List of NotificationAttachment to List of NotificationAttachmentEntity.
    /// </summary>
    /// <param name="notificationAttachments"> NotificationAttachment entities. </param>
    /// <returns> NotificationAttachementEntities. </returns>
    public static IEnumerable<NotificationAttachmentEntity> ToNotificationAttachmentEntities(IEnumerable<NotificationAttachment> notificationAttachments)
    {
        if (notificationAttachments == null)
        {
            return null;
        }

        return notificationAttachments.ToList().Select(e => (e != null ? new NotificationAttachmentEntity()
        {
            FileBase64 = e.FileBase64,
            FileName = e.FileName,
            IsInline = e.IsInline,
        }
        : null));
    }


}