

using System;
using System.Collections.Generic;
using System.Linq;

public static class CollectionExtensions
{
    public static T GetRandomElement<T>(this IReadOnlyCollection<T> collection, Random random, int startIndex = 0)
    {
        var index = random.Next(startIndex, collection.Count);
        return collection.Skip(index).FirstOrDefault();
    }

    public static T GetRandomListItem<T>(this IList<T> list, Random random, int startIndex = 0)
    {
        var index = random.Next(startIndex, list.Count);
        return list[index];
    }
}