using Microsoft.AspNetCore.Hosting;
using RPSLS.Game.Api.Services;
using RPSLS.Game.Api.Data;

var builder = WebApplication.CreateBuilder(args);
//builder.WebHost.ConfigureKestrel(options =>
//{
//    var ()
//})

// Add services to the container.

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
    .AddSingleton<IMatchesCacheService, MatchesCacheService>()
    .AddApplicationInsightsTelemetry();
builder.Services.AddControllers();

builder.Services.AddScoped<IMatchesRepository>(sp => new MatchesRepository(
    builder.Configuration["cosmos-constr"],
    sp.GetService<IMatchesCacheService>(),
    sp.GetService<ILoggerFactory>()));

builder.Services.AddGrpc();

builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseAuthorization();
app.UseHealthChecks("/health");
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapGet("api/games", async (string player, int limit, IMatchesRepository resultDao) =>
{
    var data = await resultDao.GetLastGamesOfPlayer(player, limit);
    var humanGames = data.Select(c => c.PlayerMove.Value).ToList();
    var challengerGames = data.Select(c => c.ChallengerMove.Value).ToList();
    return new { 
        humanGames,
        challengerGames
    };
});

app.Run();
