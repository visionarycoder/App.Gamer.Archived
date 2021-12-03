using System.Collections.Generic;

namespace Gamer.Framework.Extensions
{
    
    public static class HashSetEx
    {

        public static void AddRange<T>(this HashSet<T> value, IEnumerable<T> newItems)
        {
    
            foreach (var item in newItems)
            {
                value.Add(item);
            }

        }

    }

}
