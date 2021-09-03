using System.Collections.Generic;
using System.Linq;

namespace Util.Caching
{
    public class SimpleCache<T>
    {

        private readonly HashSet<T> cache = new HashSet<T>();

        public long Count => cache.Count;

        public bool Contains(T obj)
        {
            return cache.Contains(obj);
        }

        public T Get(T obj)
        {
            return Contains(obj) ? obj : default(T);
        }

        public IEnumerable<T> All()
        {
            return cache.AsEnumerable();
        }

        public void Add(IEnumerable<T> items)
        {
            foreach (var item in items.Where(i => i != null))
                Add(item);
        }

        public bool Any()
        {
            return cache.Any();
        }

        public void Clear()
        {
            cache.Clear();
        }

        public void Add(T item)
        {
            if (item == null || Contains(item))
                return;
            cache.Add(item);
        }

        public void AddRange(IEnumerable<T> items)
        {
            foreach (var item in items)
                Add(item);
        }

        public void Remove(T item)
        {
            if (Contains(item))
                cache.Remove(item);
        }

    }
}