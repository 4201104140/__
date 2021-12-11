using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(o => o.LoginPath = new PathString("/login"))
    .AddGoogle(o =>
    {
        o.ClientId = "407576620147-2d11d7hvqc3fnudniv4q6hag23uftqk6.apps.googleusercontent.com";
        o.ClientSecret = "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";
        o.AuthorizationEndpoint += "?prompt=consent";
        o.AccessType = "offline";
        o.SaveTokens = true;
        o.Events = new OAuthEvents()
        {
            OnRemoteFailure = HandleOnRemoteFailure,
        };
        o.ClaimActions.MapJsonSubKey("urn:google:image", "image", "url");
        o.ClaimActions.Remove(ClaimTypes.GivenName);
    });




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.Map("/login", signinApp =>
{
    signinApp.Run(async context =>
    {
        var authType = context.Request.Query["authscheme"];
        if (!string.IsNullOrEmpty(authType))
        {
            // By default the client will be redirect back to the URL that issued the challenge (/login?authtype=foo),
            // send them to the home page instead (/).
            await context.ChallengeAsync(authType, new AuthenticationProperties() { RedirectUri = "/" });
            return;
        }

        var response = context.Response;
        response.ContentType = "text/html";
        await response.WriteAsync("<html><body>");
        await response.WriteAsync("Choose an authentication scheme: <br>");
        var schemeProvider = context.RequestServices.GetRequiredService<IAuthenticationSchemeProvider>();
        foreach (var provider in await schemeProvider.GetAllSchemesAsync())
        {
            await response.WriteAsync("<a href=\"?authscheme=" + provider.Name + "\">" + (provider.DisplayName ?? "(suppressed)") + "</a><br>");
        }
        await response.WriteAsync("</body></html>");
    });
});

app.Map("/refresh_token", signinApp =>
{
    signinApp.Run(async context =>
    {
        var response = context.Response;

        var userResult = await context.AuthenticateAsync();
        var user = userResult.Principal;
        var authProperties = userResult.Properties;

        if (!userResult.Succeeded || user == null || !user.Identities.Any(identity => identity.IsAuthenticated))
        {
            await context.ChallengeAsync();

            return;
        }

        var currentAuthType = user.Identities.First().AuthenticationType;
        if (string.Equals(GoogleDefaults.AuthenticationScheme, currentAuthType))
        {
            var refreshToken = authProperties!.GetTokenValue("refresh_token");

            if (string.IsNullOrEmpty(refreshToken))
            {
                response.ContentType = "text/html";
                await response.WriteAsync("<html><body>");
                await response.WriteAsync("No refresh_token is available.<br>");
                await response.WriteAsync("<a href=\"/\">Home</a>");
                await response.WriteAsync("</body></html>");
                return;
            }

            var options = await GetOAuthOptionsAsync(context, currentAuthType!);

            var pairs = new Dictionary<string, string>()
            {
                { "client_id", options.ClientId },
                { "client_secret", options.ClientSecret },
                { "grant_type", "refresh_token" },
                { "refresh_token", refreshToken }
            };
            var content = new FormUrlEncodedContent(pairs);
            var refreshResponse = await options.Backchannel.PostAsync(options.TokenEndpoint, content, context.RequestAborted);
            refreshResponse.EnsureSuccessStatusCode();

            using var payload = JsonDocument.Parse(await refreshResponse.Content.ReadAsStringAsync());
            authProperties!.UpdateTokenValue("access_token", payload.RootElement.GetString("access_token")!);
            refreshToken = payload.RootElement.GetString("refresh_token");
            if (!string.IsNullOrEmpty(refreshToken))
            {
                authProperties!.UpdateTokenValue("refresh_token", refreshToken);
            }
            if (payload.RootElement.TryGetProperty("expires_in", out var property) && property.TryGetInt32(out var seconds))
            {
                var expiresAt = DateTimeOffset.UtcNow + TimeSpan.FromSeconds(seconds);
                authProperties!.UpdateTokenValue("expires_at", expiresAt.ToString("o", CultureInfo.InvariantCulture));
            }
            await context.SignInAsync(user, authProperties);

            await PrintRefreshedTokensAsync(response, payload, authProperties!);

            return;
        }

        response.ContentType = "text/html";
        await response.WriteAsync("<html><body>");
        await response.WriteAsync("Refresh has not been implemented for this provider.<br>");
        await response.WriteAsync("<a href=\"/\">Home</a>");
        await response.WriteAsync("</body></html>");
    });
});

app.Run();

async Task HandleOnRemoteFailure(RemoteFailureContext context)
{
    context.Response.StatusCode = 500;
    context.Response.ContentType = "text/html";
    await context.Response.WriteAsync("<html><body>");
    await context.Response.WriteAsync("A remote failure has occurred: <br>" +
        context.Failure?.Message.Split(Environment.NewLine).Select(s => HtmlEncoder.Default.Encode(s) + "<br>").Aggregate((s1, s2) => s1 + s2));

    if (context.Properties != null)
    {
        await context.Response.WriteAsync("Properties:<br>");
        foreach (var pair in context.Properties.Items)
        {
            await context.Response.WriteAsync($"-{ HtmlEncoder.Default.Encode(pair.Key)}={ HtmlEncoder.Default.Encode(pair.Value!)}<br>");
        }
    }

    await context.Response.WriteAsync("<a href=\"/\">Home</a>");
    await context.Response.WriteAsync("</body></html>");

    context.HandleResponse();
}

Task<OAuthOptions> GetOAuthOptionsAsync(HttpContext context, string currentAuthType)
{
    return currentAuthType switch
    {
        GoogleDefaults.AuthenticationScheme => Task.FromResult<OAuthOptions>(context.RequestServices.GetRequiredService<IOptionsMonitor<GoogleOptions>>().Get(currentAuthType)),
        _ => throw new NotImplementedException(currentAuthType)
    };
}

async Task PrintRefreshedTokensAsync(HttpResponse response, JsonDocument payload, AuthenticationProperties authProperties)
{
    response.ContentType = "text/html";
    await response.WriteAsync("<html><body>");
    await response.WriteAsync("Refreshed.<br>");
    await response.WriteAsync(HtmlEncoder.Default.Encode(payload.RootElement.ToString()).Replace(",", ",<br>") + "<br>");

    await response.WriteAsync("<br>Tokens:<br>");

    await response.WriteAsync("Access Token: " + authProperties.GetTokenValue("access_token") + "<br>");
    await response.WriteAsync("Refresh Token: " + authProperties.GetTokenValue("refresh_token") + "<br>");
    await response.WriteAsync("Token Type: " + authProperties.GetTokenValue("token_type") + "<br>");
    await response.WriteAsync("expires_at: " + authProperties.GetTokenValue("expires_at") + "<br>");

    await response.WriteAsync("<a href=\"/\">Home</a><br>");
    await response.WriteAsync("<a href=\"/refresh_token\">Refresh Token</a><br>");
    await response.WriteAsync("</body></html>");
}