
using System.Text.Json.Serialization;

namespace RPSLS.Game.Api.Data.Models;

public class MatchDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("whenUtc")]
    public DateTime WhenUtc { get; set; }

    [JsonPropertyName("challenger")]
    public ChallengerDto Challenger { get; set; }

    [JsonPropertyName("playerName")]
    public string PlayerName { get; set; }

    [JsonPropertyName("playerMove")]
    public MoveDto PlayerMove { get; set; }

    [JsonPropertyName("challengerMove")]
    public MoveDto ChallengerMove { get; set; }

    [JsonPropertyName("playFabMatchId")]
    public string PlayFabMatchId { get; set; }

    [JsonPropertyName("result")]
    public ResultDto Result { get; set; }
}
