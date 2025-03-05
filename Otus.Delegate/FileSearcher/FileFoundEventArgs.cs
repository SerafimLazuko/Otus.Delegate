namespace Otus.Delegate.FileSearcher;

public class FileFoundEventArgs(string FileName) : EventArgs
{
    public string FileName { get; } = FileName;
}