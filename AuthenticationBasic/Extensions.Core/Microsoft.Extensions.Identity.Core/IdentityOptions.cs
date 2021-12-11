namespace Microsoft.AspNetCore.Identity;

public class IdentityOptions
{
    /// <summary>
    /// Gets or sets the <see cref="ClaimsIdentityOptions"/> for the identity system.
    /// </summary>
    /// <value>
    /// The <see cref="ClaimsIdentityOptions"/> for the identity system.
    /// </value>
    public ClaimsIdentityOptions ClaimsIdentity { get; set; } = new ClaimsIdentityOptions();

    /// <summary>
    /// Gets or sets the <see cref="UserOptions"/> for the identity system.
    /// </summary>
    /// <value>
    /// The <see cref="UserOptions"/> for the identity system.
    /// </value>
    public UserOptions User { get; set; } = new UserOptions();
}
