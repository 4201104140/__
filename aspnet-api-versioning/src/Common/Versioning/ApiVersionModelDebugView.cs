#pragma warning disable CA1812

namespace Microsoft.AspNetCore.Mvc.Versioning
{
    using System;
    using static System.String;

    sealed class ApiVersionModelDebugView
    {
        const string Comma = ", ";
        readonly ApiVersionModel model;

        public ApiVersionModelDebugView(ApiVersionModel model) => this.model = model;

        public bool VersionNeutral => model.IsApiVersionNeutral;

        public string Declared => Join(Comma, model.)
    }
}
