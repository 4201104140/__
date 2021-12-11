using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Identity.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.Identity;

/// <summary>
/// Provides the APIs for managing user in a persistence store.
/// </summary>
/// <typeparam name="TUser">The type encapsulating a user.</typeparam>
public class UserManager<TUser> : IDisposable where TUser : class
{
    private bool _disposed;

    private readonly IServiceProvider _services;

    /// <summary>
    /// The cancellation token used to cancel operations.
    /// </summary>
    protected virtual CancellationToken CancellationToken => CancellationToken.None;

    /// <summary>
    /// Gets or sets the persistence store the manager operates over.
    /// </summary>
    /// <value>The persistence store the manager operates over.</value>
    protected internal IUserStore<TUser> Store { get; set; }

    /// <summary>
    /// The <see cref="ILogger"/> used to log messages from the manager.
    /// </summary>
    /// <value>
    /// The <see cref="ILogger"/> used to log messages from the manager.
    /// </value>
    public virtual ILogger Logger { get; set; }

    /// <summary>
    /// The <see cref="IdentityOptions"/> used to configure Identity.
    /// </summary>
    public IdentityOptions Options { get; set; }

    public virtual IQueryable<TUser> Users
    {
        get
        {
            var queryableStore 
        }
    }

    /// <summary>
    /// Releases all resources used by the user manager.
    /// </summary>
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public virtual string GetUserName(ClaimsPrincipal principal)
    {
        if (principal == null)
        {
            throw new ArgumentNullException(nameof(principal));
        }
        return principal.FindFirstValue(Options.ClaimsIdentity.UserIdClaimType);
    }

    /// <summary>
    /// Throws if this class has been disposed.
    /// </summary>
    protected void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }
    }
}

