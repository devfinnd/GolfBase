using System.Text.Json;
using Serilog;

namespace GolfBase.CompetitionClient.Game;

public sealed class SaveFileWatcher
{
    private readonly FileInfo _saveFile;
    private readonly FileSystemWatcher _watcher;
    private SaveFile? _lastSave;

    public SaveFileWatcher(string saveFilePath)
    {
        _saveFile = new FileInfo(saveFilePath);

        if (!_saveFile.Exists)
        {
            throw new FileNotFoundException("Save file does not exist", saveFilePath);
        }

        _watcher = new FileSystemWatcher
        {
            Path = _saveFile.DirectoryName!,
            NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size,
            Filter = _saveFile.Name
        };

        _lastSave = JsonSerializer.Deserialize<SaveFile>(File.ReadAllText(_saveFile.FullName));
    }

    public void Watch(Action<SaveFile?, SaveFile?> callback)
    {
        _watcher.Changed += (_, args) =>
        {
            string? saveFileContents = GetSaveFileContents();
            if (saveFileContents == null) return;

            SaveFile? nextSave = JsonSerializer.Deserialize<SaveFile>(saveFileContents);
            callback(_lastSave, nextSave);
            _lastSave = nextSave;
        };

        _watcher.EnableRaisingEvents = true;
    }

    private string? GetSaveFileContents()
    {
        try
        {
            return File.ReadAllText(_saveFile.FullName);
        }
        catch (Exception e)
        {
            Log.Debug("Failed to read save file: {Message}", e.Message);
            return null;
        }
    }
}
