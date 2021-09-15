namespace RPSLS.Game.Api.Data;

using RPSLS.Game.Api.Data.Models;

public interface IMatchesRepository
{
    Task CreateMatch(string matchId, string username, string challenger);
    Task<MatchDto> GetMatch(string matchId);
    Task<MatchDto> SaveMatchPick(string matchId, string username, int pick);
    Task<IEnumerable<MatchDto>> GetLastGamesOfPlayer(string player, int limit);
}
