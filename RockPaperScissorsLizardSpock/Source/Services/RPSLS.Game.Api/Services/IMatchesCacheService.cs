namespace RPSLS.Game.Api.Services;

using RPSLS.Game.Api.Data.Models;

public interface IMatchesCacheService
{
    void CreateMatch(MatchDto matchDto);
    MatchDto GetMatch(string matchId);
    MatchDto UpdateMatch(MatchDto updatedMatch);
    void DeleteMatch(string matchId);
}
