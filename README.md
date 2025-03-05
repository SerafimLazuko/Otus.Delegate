
# File Searcher

Этот проект реализует поиск файлов в указанной директории и её подкаталогах с использованием токенов отмены, событий и функции расширения для нахождения максимального элемента.

## Класс `FileEventArgs`

Класс `FileEventArgs` наследуется от `EventArgs` и содержит имя найденного файла.

```csharp
public class FileEventArgs : EventArgs
{
    public string FileName { get; }

    public FileEventArgs(string fileName)
    {
        FileName = fileName;
    }
}
```

## Статический класс `FileSearcher`

Статический класс `FileSearcher` выполняет рекурсивный обход файлов в директории и вызывает событие `FileFound` при нахождении каждого файла.

### Событие `FileFound`

Событие `FileFound` вызывается при нахождении каждого файла и передаёт аргументы типа `FileEventArgs`.

### Метод `SearchFiles`

Метод `SearchFiles` принимает директорию и токен отмены, и возвращает список найденных файлов.

### Пример использования

```csharp
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
```

## Функция расширения `GetMax`

Метод расширения `GetMax` находит и возвращает максимальный элемент коллекции на основе делегата, преобразующего элемент в значение типа `float`.

```csharp
public static T GetMax<T>(this IEnumerable<T> collection, Func<T, float> convertToNumber) where T : class
{
    if (collection == null || !collection.Any())
    {
        throw new ArgumentException("Collection is empty or null.");
    }

    T maxItem = null;
    float maxValue = float.MinValue;

    foreach (var item in collection)
    {
        float value = convertToNumber(item);
        if (value > maxValue)
        {
            maxValue = value;
            maxItem = item;
        }
    }

    return maxItem;
}
```

## Пример использования

В этом примере выводятся найденные файлы в консоль, производится отмена поиска после нахождения 15 файлов, а также используется метод `GetMax` для нахождения файла с максимальным размером.

