using GolfBase.ApiContracts.Players.CreatePlayer;
using GolfBase.CompetitionClient.Abstractions;
using Refit;
using Spectre.Console;
using Spectre.Console.Cli;

namespace GolfBase.CompetitionClient.Commands;

public sealed class SetupCommand : AsyncCommand<SetupCommand.Settings>
{
    public sealed class Settings : CommandSettings
    {
        [CommandArgument(0, "[PLAYER_ID]")] public Guid? PlayerId { get; init; }

        [CommandArgument(1, "[PLAYER_NAME]")] public string? PlayerName { get; init; }

        [CommandArgument(2, "[SAVE_FILE_PATH]")]
        public string? SaveFilePath { get; init; }
    }

    private readonly IGolfBaseApiClient _apiClient;
    private readonly ISettingsStorage _settingsStorage;

    public SetupCommand(IGolfBaseApiClient apiClient, ISettingsStorage settingsStorage)
    {
        _apiClient = apiClient;
        _settingsStorage = settingsStorage;
    }

    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        Guid playerId = settings.PlayerId ?? await PromptForPlayerId();
        string saveFilePath = settings.SaveFilePath ?? PromptForSaveFilePath();

        await _settingsStorage.UpdateClientSettings(clientSettings =>
        {
            clientSettings.PlayerId = playerId;
            clientSettings.SaveFilePath = saveFilePath;
        });

        return 0;
    }

    private async Task<Guid> PromptForPlayerId()
    {
        if (AnsiConsole.Confirm("Do you already have an account?"))
        {
            return AnsiConsole.Ask<Guid>("PlayerId:");
        }

        string playerName = AnsiConsole.Ask<string>("Your name for the Leaderboard:");
        IApiResponse<CreatePlayerResponse> response = await _apiClient.CreatePlayer(new CreatePlayerRequest(
            Name: playerName
        ));

        if (response is { IsSuccessStatusCode: true, Content.PlayerId: var playerId })
        {
            return playerId;
        }


        AnsiConsole.WriteException(new Exception("Failed to create player"));
        Environment.Exit(1);
        return Guid.Empty;
    }

    private static string PromptForSaveFilePath() => AnsiConsole.Prompt(new TextPrompt<string>("Path to your GWYF save file:").Validate(path => new FileInfo(path) switch
    {
        { Exists: true, Name: "gwyf.sav" } => ValidationResult.Success(),
        { Exists: true } => AnsiConsole.Confirm("This file does not seem to be a GWYF save file. Continue anyway?")
            ? ValidationResult.Success()
            : ValidationResult.Error(),
        _ => ValidationResult.Error("This file does not exist!")
    }));
}
