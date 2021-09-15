namespace RPSLS.Game.Api.Data;

using GameApi.Proto;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using RPSLS.Game.Api.Data.Models;
using RPSLS.Game.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

public class MatchesRepository : IMatchesRepository
{
    private const string DatabaseName = "rpsls";
    private readonly string _constr;
    private readonly IMatchesCacheService _cacheService;
    private readonly ILogger<MatchesRepository> _logger;

    public MatchesRepository(string constr, IMatchesCacheService cacheService, ILoggerFactory loggerFactory)
    {
        _constr = constr;
        _cacheService = cacheService;
        _logger = loggerFactory.CreateLogger<MatchesRepository>();
    }

    public async Task CreateMatch(string matchId, string username, string challenger)
    {
        var dto = new MatchDto();
        dto.Challenger.Name = challenger;
        dto.Challenger.Type = "human";
        dto.PlayerName = username;
        dto.PlayFabMatchId = matchId;
        dto.Result.Value = (int)Result.Pending;
        dto.Result.Winner = Enum.GetName(typeof(Result), Result.Pending);

        _cacheService.CreateMatch(dto);
        if (_constr == null)
        {
            _logger.LogInformation("+++ Cosmos constr is null. Doc that would be written is:");
            _logger.LogInformation(JsonSerializer.Serialize(dto));
            _logger.LogInformation("+++ Nothing was written on Cosmos");
            return;
        }

        var cResponse = await GetContainer();
        var response = await cResponse.Container.CreateItemAsync(dto);
        if (response.StatusCode != System.Net.HttpStatusCode.OK &&
            response.StatusCode != System.Net.HttpStatusCode.Created)
        {
            _logger.LogInformation($"Cosmos save attempt resulted with StatusCode {response.StatusCode}.");
        }
    }

    public async Task<MatchDto> GetMatch(string matchId)
    {
        var result = _cacheService.GetMatch(matchId);
        if (result != null) return result;
        if (_constr == null) return CreateMissing(matchId);
        var cResponse = await GetContainer();
        var matches = cResponse.Container.GetItemLinqQueryable<MatchDto>(allowSynchronousQueryExecution: true)
            .Where(m => m.PlayFabMatchId == matchId).ToList();
        return matches.OrderByDescending(m => m.WhenUtc).FirstOrDefault();
    }

    public Task<MatchDto> SaveMatchPick(string matchId, string username, int pick)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<MatchDto>> GetLastGamesOfPlayer(string player, int limit)
    {
        if (_constr == null)
        {
            _logger.LogInformation($"Cosmos constr is null. No games returned for player {player}.");
            return Enumerable.Empty<MatchDto>();
        }

        var cResponse = await GetContainer();
        var sqlQueryText = $"SELECT * FROM g WHERE g.playerName = '{player}' AND (NOT(IS_DEFINED(g.playFabMatchId)) OR IS_NULL(g.playFabMatchId)) ORDER BY g.whenUtc DESC";
        var queryDefinition = new QueryDefinition(sqlQueryText);
        var rs = cResponse.Container.GetItemQueryIterator<MatchDto>(queryDefinition);
        var results = new List<MatchDto>();
        while (rs.HasMoreResults && (limit <= 0 || results.Count < limit))
        {
            var items = await rs.ReadNextAsync();
            results.AddRange(items);
        }

        return limit > 0 ? results.Take(limit).ToList() : results.ToList();
    }

    private async Task<ContainerResponse> GetContainer()
    {
        var client = new CosmosClient(_constr);
        var db = client.GetDatabase(DatabaseName);
        var cprops = new ContainerProperties()
        {
            Id = "results",
            PartitionKeyPath = "/playerName"
        };
        return await db.CreateContainerIfNotExistsAsync(cprops);
    }

    private MatchDto CreateMissing(string matchId)
    {
        var dto = new MatchDto();
        dto.Challenger.Name = "-";
        dto.Challenger.Type = "human";
        dto.PlayFabMatchId = matchId;
        dto.PlayerName = "-";
        return dto;
    }
}
