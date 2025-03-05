namespace Otus.Delegate.FileSearcher;

public static class FileSearcher
{
    public static event EventHandler<FileFoundEventArgs>? FileFound;

    public static List<string> SearchFiles(string directory, CancellationToken cancellationToken)
    {
        var foundFiles = new List<string>();
        SearchDirectory(directory, cancellationToken, foundFiles);
        return foundFiles;
    }

    private static void SearchDirectory(string directory, CancellationToken cancellationToken, List<string> foundFiles)
    {
        if (cancellationToken.IsCancellationRequested)
            return;

        foreach (var file in Directory.GetFiles(directory))
        {
            if (cancellationToken.IsCancellationRequested)
                return;

            foundFiles.Add(file);
            OnFileFound(file);
        }

        foreach (var subDirectory in Directory.GetDirectories(directory))
        {
            if (cancellationToken.IsCancellationRequested)
                return;

            SearchDirectory(subDirectory, cancellationToken, foundFiles);
        }
    }

    private static void OnFileFound(string file)
    {
        FileFound?.Invoke(null, new FileFoundEventArgs(file));
    }
}