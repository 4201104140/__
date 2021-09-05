

namespace NotificationService.Common.Utility;

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/// <summary>
/// Extensions class.
/// </summary>
public static class Extensions
{
    private static readonly Regex WhiteSpaceRegex = new Regex(@"\s+");

    /// <summary>
    ///  Removes all the whitespaces from given string.
    /// </summary>
    /// <param name="value">input string.</param>
    /// <returns>string without whitespaces.</returns>
    public static bool HasWhitespaces(this string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return false;
        }

        return WhiteSpaceRegex.IsMatch(value);
    }

    /// <summary>
    /// Validates email address.
    /// </summary>
    /// <param name="emailId">emailId string to validate.</param>
    /// <returns>Validation status of email.</returns>
    public static bool IsValidEmail(this string emailId)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(emailId);
            return addr.Address == emailId;
        }
#pragma warning disable CA1031 // Do not catch general exception types
        catch
#pragma warning restore CA1031 // Do not catch general exception types
        {
            return false;
        }
    }
}