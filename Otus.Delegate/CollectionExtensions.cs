namespace Otus.Delegate;

public static class CollectionExtensions
{
    public static T GetMax<T>(this IEnumerable<T> collection, Func<T, float> convertToNumber) where T : class
    {
        if (collection == null || !collection.Any())
        {
            throw new ArgumentException("Collection is empty or null.");
        }

        var maxItem = collection.First();
        var maxValue = convertToNumber(maxItem);

        foreach (var item in collection)
        {
            var value = (float) convertToNumber(item);
            
            if(value > maxValue)
            {
                maxItem = item;
                maxValue = value;
            }
        }
        
        return maxItem;
    }
}