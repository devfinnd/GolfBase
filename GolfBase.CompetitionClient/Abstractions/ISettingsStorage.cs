using GolfBase.CompetitionClient.Settings;

namespace GolfBase.CompetitionClient.Abstractions;

public interface ISettingsStorage
{
    public Task<ClientSettings?> GetClientSettings();

    public Task UpdateClientSettings(Action<ClientSettings> updater);
}
