using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Identity;

public interface IUserStore<TUser> : IDisposable where TUser : class
{
    Task<string> GetUserIdAsync(TUser user, CancellationToken cancellationToken);

    Task<string> GetUserNameAsync(TUser user, CancellationToken cancellationToken);

    Task SetUserNameAsync(TUser user, string userName, CancellationToken cancellationToken);
}

