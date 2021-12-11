namespace Microsoft.AspNetCore.Mvc.Versioning
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// Represents the API version information for an ASP.NET controller or action.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplayText}")]
    [DebuggerTypeProxy(typeof())]
    public sealed partial class ApiVersionModel
    {
        const int EmptyModel = 2;
        static readonly Lazy<ApiVersionModel> emptyVersion = new Lazy<ApiVersionModel>(() => new ApiVersionModel(EmptyModel));

        public ApiVersionModel(int kind)
        {
            switch (kind)
            {
                case EmptyModel:

            }
        }

        public static ApiVersionModel Empty =>

        /// <summary>
        /// Gets a value indicating whether the controller is API version neutral.
        /// </summary>
        /// <value>True if the controller is API version neutral (e.g. "unaware"); otherwise, false.</value>
        /// <remarks>A controller is API version neutral only if the <see cref="ApiVersionNeutralAttribute"/> has been applied.</remarks>
        public bool IsApiVersionNeutral { get; }
    }
}
