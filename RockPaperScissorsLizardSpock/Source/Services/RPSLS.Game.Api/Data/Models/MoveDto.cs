
using System.Text.Json.Serialization;

namespace RPSLS.Game.Api.Data.Models;
public class MoveDto
{
    [JsonPropertyName("value")]
    public int Value { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; }
}
