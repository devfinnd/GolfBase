using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.JsonDiffPatch;
using System.Text.Json.Nodes;
using GolfBase.CompetitionClient.Abstractions;
using GolfBase.CompetitionClient.Game;
using GolfBase.CompetitionClient.Settings;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Json;

namespace GolfBase.CompetitionClient.Commands;

public sealed class CompetetiveGameCommand : AsyncCommand
{
    private readonly ISettingsStorage _settingsStorage;

    public CompetetiveGameCommand(ISettingsStorage settingsStorage)
    {
        _settingsStorage = settingsStorage;
    }

    public override async Task<int> ExecuteAsync(CommandContext context)
    {
        ClientSettings? clientSettings = await _settingsStorage.GetClientSettings();
        if (clientSettings is null)
        {
            AnsiConsole.MarkupLine("Please run [bold]setup[/] first!");
            return 1;
        }

        AnsiConsole.MarkupLine($"[bold yellow]PlayerId:[/] {clientSettings.PlayerId}");
        AnsiConsole.MarkupLine($"[bold yellow]SaveFilePath:[/] {clientSettings.SaveFilePath}");

        Process gameProcess = null!;

        await AnsiConsole.Status().StartAsync("Launching game...", async ctx => { gameProcess = await Launcher.LaunchGame(); });

        AnsiConsole.MarkupLine("[bold green]Game launched![/]");
        await Task.Delay(TimeSpan.FromSeconds(1));

        SaveFileWatcher watcher = new(clientSettings.SaveFilePath);
        watcher.Watch((lastSave, nextSave) =>
        {
            if (lastSave == null || nextSave == null) return;

            (JsonNode? stats, JsonNode? newStats) diff = GetDiff(lastSave, nextSave);

            Layout layout = new Layout("Root")
                .SplitColumns(
                    new Layout("Left"),
                    new Layout("Right"));

            if (diff.stats == null && diff.newStats == null) return;

            if (diff.stats != null)
            {
                layout["Left"].Update(
                    new Panel(new JsonText(diff.stats.ToJsonString()))
                        .Expand()
                        .Header("Stats"));
            }

            if (diff.newStats != null)
            {
                layout["Right"].Update(
                    new Panel(new JsonText(diff.newStats.ToJsonString()))
                        .Expand()
                        .Header("NewStats"));
            }

            AnsiConsole.Write(layout);
        });

        AnsiConsole.MarkupLine($"Tracking changes to [bold green]{clientSettings.SaveFilePath}[/]");

        await gameProcess.WaitForExitAsync();
        AnsiConsole.WriteLine("Game exited, closing in 5 seconds...");
        await Task.Delay(TimeSpan.FromSeconds(5));

        return 0;
    }

    private (JsonNode? stats, JsonNode? newStats) GetDiff(SaveFile lastSave, SaveFile nextSave)
    {
        JsonSerializerOptions jsonSerializerOptions = new() { WriteIndented = true };
        string prevStats = JsonSerializer.Serialize(lastSave.GetStats(), jsonSerializerOptions);
        string currStats = JsonSerializer.Serialize(nextSave.GetStats(), jsonSerializerOptions);

        string prevNewStats = JsonSerializer.Serialize(lastSave.GetNewStats(), jsonSerializerOptions);
        string currNewStats = JsonSerializer.Serialize(nextSave.GetNewStats(), jsonSerializerOptions);

        JsonNode? statsDiff = JsonNode.Parse(prevStats).Diff(JsonNode.Parse(currStats));
        JsonNode? newStatsDiff = JsonNode.Parse(prevNewStats).Diff(JsonNode.Parse(currNewStats));

        return (statsDiff, newStatsDiff);
    }
}
