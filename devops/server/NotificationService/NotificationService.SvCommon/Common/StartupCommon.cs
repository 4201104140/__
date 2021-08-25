using Common.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotificationService.Common;

namespace NotificationService.SvCommon.Common;

public class StartupCommon
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StartupCommon"/> class.
    /// </summary>
    /// <param name="application">service/application name (Handler, Service).</param>
    public StartupCommon(string application)
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables();

        var config = builder.Build();

        this.Configuration = builder.Build();
    }

    /// <summary>
    /// Gets the application configuration.
    /// </summary>
    public IConfiguration Configuration { get; }

    /// <summary>
    /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// </summary>
    /// <param name="app">An instance of <see cref="IApplicationBuilder"/>.</param>
    /// <param name="env">An instance of <see cref="IWebHostEnvironment"/>.</param>
    public static void ConfigureCommon(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            _ = app.UseDeveloperExceptionPage();
        }

        _ = app.UseMiddleware<ExceptionMiddleware>();

        _ = app.UseHttpsRedirection();

        _ = app.UseFileServer();

        _ = app.UseRouting();

        _ = app.UseAuthentication();

        _ = app.UseAuthorization();
    }

    public void ConfigureServicesCommon(IServiceCollection services)
    {
        _ = services.AddAuthorization(configure =>
        {
            configure.AddPolicy(ApplicationConstants.AppNameAuthorizePolicy, policy =>
            {
                policy.Requirements.Add(new AppNameAuthorizeRequirement());
            });
        });

        _ = services.AddControllers();

    }
}
