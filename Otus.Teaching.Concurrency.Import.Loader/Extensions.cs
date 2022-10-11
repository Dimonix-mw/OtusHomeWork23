using System.Collections.Generic;
using System.Linq;

namespace Otus.Teaching.Concurrency.Import.Loader
{
    public static class Extensions
    {
        public static List<List<T>> Partition<T>(this List<T> values, int parts)
        {
            int i = 0;
            var partition = from item in values
                            group item by i++ % parts into part
                            select part.ToList();
            return partition.ToList();
        }
    }
}