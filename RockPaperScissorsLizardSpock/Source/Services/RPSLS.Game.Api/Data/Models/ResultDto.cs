using System.Text.Json.Serialization;

namespace RPSLS.Game.Api.Data.Models;

public class ResultDto
{
    [JsonPropertyName("value")]
    public int Value { get; set; }
    
    [JsonPropertyName("winner")]
    public string Winner { get; set; }
}
