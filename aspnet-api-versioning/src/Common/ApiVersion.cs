namespace Microsoft.AspNetCore.Mvc
{
    using Microsoft.AspNetCore.Mvc.Versioning;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using static System.DateTime;
    using static System.Globalization.CultureInfo;
    using static System.String;
    using static System.Text.RegularExpressions.Regex;
    using static System.Text.RegularExpressions.RegexOptions;

    /// <summary>
    /// Represents the application programming interface (API) version of a service.
    /// </summary>
    public class ApiVersion : IEquatable<ApiVersion>, IComparable<ApiVersion>, IFormattable
    {
        const int Prime = 397;
        const string ParsePattern = @"^(\d{4}-\d{2}-\d{2})?\.?(\d{0,9})\.?(\d{0,9})\.?-?(.*)$";
        const string GroupVersionFormat = "yyyy-MM-dd";
        int hashCode;

        ApiVersion()
        {
            const int MajorVersion = int.MaxValue;
            const int MinorVersion = int.MaxValue;
            var groupVersion = MaxValue;

            hashCode = groupVersion.GetHashCode();
            hashCode = ( hashCode * Prime ) ^ MajorVersion;
            hashCode = ( hashCode * Prime ) ^ MinorVersion;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiVersion"/> class.
        /// </summary>
        /// <param name="groupVersion">The group version.</param>
        public ApiVersion( DateTime groupVersion )
            : this(new DateTime?(groupVersion), null, null, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiVersion"/> class.
        /// </summary>
        /// <param name="groupVersion">The group version.</param>
        /// <param name="status">The version status.</param>
        public ApiVersion( DateTime groupVersion, string status )
            : this(new DateTime?(groupVersion), null, null, status) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiVersion"/> class.
        /// </summary>
        /// <param name="majorVersion">The major version.</param>
        /// <param name="minorVersion">The minor version.</param>
        public ApiVersion( int majorVersion, int minorVersion )
            : this(null, new int?(majorVersion), new int?(minorVersion), null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiVersion"/> class.
        /// </summary>
        /// <param name="majorVersion">The major version.</param>
        /// <param name="minorVersion">The minor version.</param>
        /// <param name="status">The version status.</param>
        public ApiVersion( int majorVersion, int minorVersion, string? status )
            : this(null, new int?(majorVersion), new int?(minorVersion), status) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiVersion"/> class.
        /// </summary>
        /// <param name="groupVersion">The group version.</param>
        /// <param name="majorVersion">The major version.</param>
        /// <param name="minorVersion">The minor version.</param>
        public ApiVersion( DateTime groupVersion, int majorVersion, int minorVersion )
            : this(new DateTime?(groupVersion), new int?(majorVersion), new int?(minorVersion), null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiVersion"/> class.
        /// </summary>
        /// <param name="groupVersion">The group version.</param>
        /// <param name="majorVersion">The major version.</param>
        /// <param name="minorVersion">The minor version.</param>
        /// <param name="status">The version status.</param>
        public ApiVersion( DateTime groupVersion, int majorVersion, int minorVersion, string? status )
            : this(new DateTime?(groupVersion), new int?(majorVersion), new int?(minorVersion), status) { }

        internal ApiVersion( DateTime? groupVersion, int? majorVersion, int? minorVersion, string? status )
        {
            if ( majorVersion.HasValue && majorVersion.Value < 0 )
            {
                throw new ArgumentOutOfRangeException(nameof(majorVersion));
            }

            if ( minorVersion.HasValue && minorVersion.Value < 0 )
            {
                throw new ArgumentOutOfRangeException(nameof(minorVersion));
            }

            if ( IsNullOrEmpty(status) )
            {
                Status = null;
            }
            else if ( !IsValidStatus(status) )
            {
                throw new ArgumentException(SR.ApiVersionBadStatus.FormatDefault(status), nameof(status));
            }
            else
            {
                Status = status;
            }

            GroupVersion = groupVersion;
            MajorVersion = majorVersion;
            MinorVersion = minorVersion;
        }

        /// <summary>
        /// Gets the default API version.
        /// </summary>
        /// <value>The default <see cref="ApiVersion">API version</see>, which is always "1.0".</value>
        public static ApiVersion Default { get; } = new ApiVersion(1, 0);

        /// <summary>
        /// Gets the neutral API version.
        /// </summary>
        /// <value>The neutral <see cref="ApiVersion">API version</see>.</value>
        public static ApiVersion Neutral { get; } = new ApiVersion();

        /// <summary>
        /// Gets the group version.
        /// </summary>
        /// <value>The group version or null.</value>
        /// <remarks>If the group version is specified, only the date component is considered.</remarks>
        public DateTime? GroupVersion { get; }

        /// <summary>
        /// Gets the major version number.
        /// </summary>
        /// <value>The major version number or <c>null</c>.</value>
        public int? MajorVersion { get; }

        /// <summary>
        /// Gets the minor version number.
        /// </summary>
        /// <value>The minor version number or <c>null</c>.</value>
        public int? MinorVersion { get; }

        int ImpliedMinorVersion => MinorVersion ?? 0;

        /// <summary>
        /// Gets the optional version status.
        /// </summary>
        /// <value>The version status.</value>
        /// <remarks>The version status typically allows services to indicate pre-release or test
        /// versions that are not release quality or guaranteed to be supported. Example values
        /// might include "Alpha", "Beta", "RC", etc.</remarks>
        public string? Status { get; }

        /// <summary>
        /// Gets a value indicating whether the specified status is valid.
        /// </summary>
        /// <param name="status">The status to evaluate.</param>
        /// <returns>True if the status is valid; otherwise, false.</returns>
        /// <remarks>The status must be alphabetic or alphanumeric, start with a letter, and contain no spaces.</remarks>
        public static bool IsValidStatus(string? status) => !IsNullOrEmpty(status) && IsMatch(status, @"^[a-zA-Z][a-zA-Z0-9]*$", Singleline);

        /// <summary>
        /// Determines whether the current object equals another object.
        /// </summary>
        /// <param name="other">The <see cref="ApiVersion">other</see> to evaluate.</param>
        /// <returns>True if the specified object is equal to the current instance; otherwise, false.</returns>
        public virtual bool Equals( ApiVersion? other ) => other != null && GetHashCode() == other.GetHashCode();

        /// <summary>
        /// Performs a comparison of the current object to another object and returns a value
        /// indicating whether the object is less than, greater than, or equal to the other.
        /// </summary>
        /// <param name="other">The <see cref="ApiVersion">other</see> object to compare to.</param>
        /// <returns>Zero if the objects are equal, one if the current object is greater than the
        /// <paramref name="other"/> object, or negative one if the current object is less than the
        /// <paramref name="other"/> object.</returns>
        /// <remarks>The version <see cref="Status">status</see> is not included in comparisons.</remarks>
        public virtual int CompareTo( ApiVersion? other )
        {
            if ( other == null )
            {
                return 1;
            }

            var result = Nullable.Compare(GroupVersion, other.GroupVersion);

            if ( result != 0 )
            {
                return result;
            }

            result = Nullable.Compare(MajorVersion, other.MajorVersion);

            if ( result != 0 )
            {
                return result;
            }

            result = ImpliedMinorVersion.CompareTo(other.ImpliedMinorVersion);

            if ( result != 0 )
            {
                return result;
            }

            if ( IsNullOrEmpty(Status) )
            {
                if ( !IsNullOrEmpty(other.Status) )
                {
                    result = 1;
                }
            }
            else if ( IsNullOrEmpty(other.Status) )
            {
                result = -1;
            }
            else
            {
                result = StringComparer.OrdinalIgnoreCase.Compare(Status, other.Status);

                if ( result < 0 )
                {
                    result = -1;
                }
                else if ( result > 0 )
                {
                    result = 1;
                }
            }

            return result;
        }

        /// <summary>
        /// Returns the text representation of the version using the specified format and format provider.
        /// <seealso cref="ApiVersionFormatProvider"/></summary>
        /// <param name="format">The format to return the text representation in. The value can be <c>null</c> or empty.</param>
        /// <param name="formatProvider">The <see cref="IFormatProvider">format provider</see> used to generate text.
        /// This implementation should typically use an <see cref="InvariantCulture">invariant culture</see>.</param>
        /// <returns>The <see cref="string">string</see> representation of the version.</returns>
        /// <exception cref="FormatException">The specified <paramref name="format"/> is not one of the supported format values.</exception>
        public virtual string ToString( string? format, IFormatProvider? formatProvider )
        {
            var provider = ApiVersionFormatProvider.GetInstance(formatProvider);
#pragma warning disable CA1062 // Validate arguments of public methods (false positive)
            return provider.Format(format, this, formatProvider);
#pragma warning restore CA1062
        }
    }
}
