
using System.Text.Json.Serialization;

namespace RPSLS.Game.Api.Data.Models;

public class ChallengerDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }
}
