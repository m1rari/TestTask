using System.Text.Json.Serialization;

namespace DataSeeder.Models;

internal sealed record ResourceModel
{
    [JsonPropertyName("male")]
    public string[] Male { get; init; }

    [JsonPropertyName("female")]
    public string[] Female { get; init; }
}