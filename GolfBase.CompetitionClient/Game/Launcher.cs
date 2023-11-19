using System.Diagnostics;
using System.Runtime.CompilerServices;
using Serilog;

namespace GolfBase.CompetitionClient.Game;

public sealed class Launcher
{
    public static async Task<Process> LaunchGame()
    {
        Task? steamProcess = Process.Start(new ProcessStartInfo
        {
            FileName = "steam://rungameid/431240",
            UseShellExecute = true
        })?.WaitForExitAsync();

        if (steamProcess is null)
        {
            Log.Error("Failed to launch GWYF via Steam");
            Environment.Exit(1);
        }

        await steamProcess;

        TimeSpan timeout = TimeSpan.FromSeconds(30);
        CancellationTokenSource cts = new(timeout);
        Process? process = Process.GetProcessesByName("Golf With Your Friends").FirstOrDefault()
                           ?? await WaitForProcessStart("Golf With Your Friends", TimeSpan.FromSeconds(1), cts.Token)
                               .FirstOrDefaultAsync(x => x != null, cts.Token);

        if (process == null)
        {
            Log.Error("Failed to launch GWYF, timed out after {Timeout} seconds", timeout.TotalSeconds);
            Environment.Exit(1);
        }

        return process;
    }

    private static async IAsyncEnumerable<Process?> WaitForProcessStart(string processName, TimeSpan retryInterval, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            Process? process = Process.GetProcessesByName(processName).FirstOrDefault();
            if (process != null) yield return process;
            await Task.Delay(retryInterval, cancellationToken);
        }
    }
}
