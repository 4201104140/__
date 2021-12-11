
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Identity;

public interface IUserValidator<TUser> where TUser : class
{
    Task<IdentityResult> ValidateAsync()
}
