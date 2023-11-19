using System.Text.Json;
using GolfBase.CompetitionClient.Abstractions;

namespace GolfBase.CompetitionClient.Settings;

public sealed class LocalSettingsStorage : ISettingsStorage
{
    private const string SettingsFileName = "settings.json";
    private const string SettingsFolderName = "settings";

    public async Task<ClientSettings?> GetClientSettings()
    {
        FileInfo settingsFile = new(Path.Combine(Directory.GetCurrentDirectory(), SettingsFolderName, SettingsFileName));
        if (!settingsFile.Exists)
        {
            return null;
        }

        string settingsJson = await File.ReadAllTextAsync(settingsFile.FullName);
        return JsonSerializer.Deserialize<ClientSettings>(settingsJson);
    }

    public async Task UpdateClientSettings(Action<ClientSettings> updater)
    {
        FileInfo settingsFile = new(Path.Combine(Directory.GetCurrentDirectory(), SettingsFolderName, SettingsFileName));
        if (!settingsFile.Exists)
        {
            settingsFile.Directory?.Create();
        }

        ClientSettings settings = await GetClientSettings() ?? new ClientSettings();
        updater(settings);
        string settingsJson = JsonSerializer.Serialize(settings);
        await File.WriteAllTextAsync(settingsFile.FullName, settingsJson);
    }
}
