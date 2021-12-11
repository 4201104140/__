using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Identity.Core;

namespace Microsoft.AspNetCore.Identity;

/// <summary>
/// Helper functions for configuring identity services.
/// </summary>
public class IdentityBuilder
{
    /// <summary>
    /// Creates a new instance of <see cref="IdentityBuilder"/>.
    /// </summary>
    /// <param name="user">The <see cref="Type"/> to use for the users.</param>
    /// <param name="services">The <see cref="IServiceCollection"/> to attach to.</param>
    public IdentityBuilder(Type user, IServiceCollection services)
    {
        UserType = user;
        Services = services;
    }

    /// <summary>
    /// Gets the <see cref="Type"/> used for users.
    /// </summary>
    /// <value>
    /// The <see cref="Type"/> used for users.
    /// </value>
    public Type UserType { get; private set; }

    /// <summary>
    /// Gets the <see cref="IServiceCollection"/> services are attached to.
    /// </summary>
    /// <value>
    /// The <see cref="IServiceCollection"/> services are attached to.
    /// </value>
    public IServiceCollection Services { get; private set; }

    private IdentityBuilder AddScoped(Type serviceType, Type concreteType)
    {
        Services.AddScoped(serviceType, concreteType);
        return this;
    }

    public virtual IdentityBuilder AddUserValidator<TValidator>() where TValidator : class
        => AddScoped(typeof())
}

