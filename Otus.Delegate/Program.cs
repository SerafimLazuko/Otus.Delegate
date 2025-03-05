using Otus.Delegate;
using Otus.Delegate.FileSearcher;

var cts = new CancellationTokenSource();
var fileCounter = 0;

FileSearcher.FileFound += (sender, args) =>
{
    Console.WriteLine($"File found: {args.FileName}");
    fileCounter++;
    
    // Пример: Отмена поиска
    if (fileCounter == 15)
    {
        cts.Cancel();
        Console.WriteLine("Search canceled.");
    }
};

Console.WriteLine("Starting file search...");
var foundFiles = FileSearcher.SearchFiles("..\\..\\..\\", cts.Token);

// Вывод всех найденных файлов
foreach (var file in foundFiles)
{
    Console.WriteLine($"Logged file: {file}");
}

// Пример использования метода GetMax
var maxFile = foundFiles.GetMax(file => new FileInfo(file).Length);
Console.WriteLine($"Max file: {maxFile}");
Console.ReadKey();