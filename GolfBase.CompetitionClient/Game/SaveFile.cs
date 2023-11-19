using System.Text.Json.Serialization;

namespace GolfBase.CompetitionClient.Game;

public sealed record SaveFile
{
    [JsonPropertyName("stats_data")] public string StatsData { get; init; }

    [JsonPropertyName("stats_data_new")] public string StatsDataNew { get; init; }

    [JsonConstructor]
    public SaveFile()
    {
    }

    public Dictionary<string, object> GetStats() => ParseStats(StatsData);
    public Dictionary<string, object> GetNewStats() => ParseStats(StatsDataNew);

    private static Dictionary<string, object> ParseStats(string statsData) => statsData
        .Split(':', StringSplitOptions.RemoveEmptyEntries)
        .Select(ExtractBy("$", "0"))
        .DistinctBy(stat => stat.Key)
        .ToDictionary(stat => stat.Key, stat => ParseValue(stat.Value));

    private static object ParseValue(string value)
    {
        if (value.Split('$', StringSplitOptions.RemoveEmptyEntries) is { Length: > 1 } subValues)
        {
            return subValues
                .Select(v => v.Split(',', 2))
                .DistinctBy(x => x.ElementAt(0))
                .ToDictionary(x => x.ElementAt(0), x => ParseValue(x.ElementAtOrDefault(1) ?? string.Empty));
        }

        if (bool.TryParse(value, out bool boolValue))
        {
            return boolValue;
        }

        if (float.TryParse(value, out float floatValue))
        {
            return floatValue;
        }

        if (decimal.TryParse(value, out decimal decimalValue))
        {
            return decimalValue;
        }

        if (long.TryParse(value, out long intValue))
        {
            return intValue;
        }

        return value;
    }

    private static Func<string, KeyValuePair<string, string>> ExtractBy(string seperator, string defaultValue = "") => text => text.Split(seperator, 2) switch
    {
        [var key, var value] => new KeyValuePair<string, string>(key, value),
        [var key] => new KeyValuePair<string, string>(key, defaultValue),
        _ => throw new Exception("Invalid line: " + text)
    };
}
