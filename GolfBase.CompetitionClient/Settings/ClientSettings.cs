namespace GolfBase.CompetitionClient.Settings;

public sealed class ClientSettings
{
    public Guid PlayerId { get; set; } = Guid.Empty;
    public string SaveFilePath { get; set; } = string.Empty;
}