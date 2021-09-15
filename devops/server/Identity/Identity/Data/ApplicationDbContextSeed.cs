
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Services.Identity.Data.Models;
using System.Text.RegularExpressions;

namespace Services.Identity.Data;
public class ApplicationDbContextSeed
{
    private readonly IPasswordHasher<ApplicationUser> _passwordHasher = new PasswordHasher<ApplicationUser>();

    public async Task SeedAsync(ApplicationDbContext context, IWebHostEnvironment env,
        ILogger<ApplicationDbContextSeed> logger, IOptions<AppSettings> settings, int? retry = 0)
    {
        int retryForAvaiability = retry.Value;

        try
        {
            var useCustomizationData = settings.Value.UseCustomizationData;
            var contentRootPath = env.ContentRootPath;
            var webroot = env.WebRootPath;

            if (!context.Users.Any())
            {
                context.Users.AddRange(useCustomizationData
                    ? GetUsersFromFile(contentRootPath, logger)
                    : GetDefaultUser());

                await context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            if (retryForAvaiability < 10)
            {
                retryForAvaiability++;

                logger.LogError(ex, "EXCEPTION ERROR while migrating {DbContextName}", nameof(ApplicationDbContext));

                await SeedAsync(context, env, logger, settings, retryForAvaiability);
            }
        }
    }

    private IEnumerable<ApplicationUser> GetUsersFromFile(string contentRootPath, ILogger logger)
    {
        string csvFileUsers = Path.Combine(contentRootPath, "Setup", "Users.csv");

        if (!File.Exists(csvFileUsers))
        {
            return GetDefaultUser();
        }

        string[] csvheaders;
        try
        {
            string[] requiredHeaders = {
                    "cardholdername", "cardnumber", "cardtype", "city", "country",
                    "email", "expiration", "lastname", "name", "phonenumber",
                    "username", "zipcode", "state", "street", "securitynumber",
                    "normalizedemail", "normalizedusername", "password"
                };
            csvheaders = GetHeaders(requiredHeaders, csvFileUsers);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "EXCEPTION ERROR: {Message}", ex.Message);

            return GetDefaultUser();
        }

        List<ApplicationUser> users = new List<ApplicationUser>();
                    //.Skip(1) // skip header column
                    //.Select(row => Regex.Split(row, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)"))
                    //.SelectTry(column => CreateApplicationUser(column, csvheaders))
                    //.OnCaughtException(ex => { logger.LogError(ex, "EXCEPTION ERROR: {Message}", ex.Message); return null; })
                    //.Where(x => x != null)
                    //.ToList();

        return users;
    }



    private IEnumerable<ApplicationUser> GetDefaultUser()
    {
        var user =
        new ApplicationUser()
        {
            Email = "demouser@microsoft.com",
            PhoneNumber = "1234567890",
            UserName = "demouser@microsoft.com",
            NormalizedEmail = "DEMOUSER@MICROSOFT.COM",
            NormalizedUserName = "DEMOUSER@MICROSOFT.COM",
            SecurityStamp = Guid.NewGuid().ToString("D"),
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, "Pass@word1");

        return new List<ApplicationUser>()
        {
            user
        };
    }

    static string[] GetHeaders(string[] requiredHeaders, string csvfile)
    {
        string[] csvheaders = File.ReadLines(csvfile).First().ToLowerInvariant().Split(',');

        if (csvheaders.Count() != requiredHeaders.Count())
        {
            throw new Exception($"requiredHeader count '{ requiredHeaders.Count()}' is different then read header '{csvheaders.Count()}'");
        }

        foreach (var requiredHeader in requiredHeaders)
        {
            if (!csvheaders.Contains(requiredHeader))
            {
                throw new Exception($"does not contain required header '{requiredHeader}'");
            }
        }

        return csvheaders;
    }
}
