using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace Util.Caching
{
    public class Cache<T>
    {

        private readonly ConcurrentDictionary<object, SemaphoreSlim> locks;

        private MemoryCache cache;

        public MemoryCacheEntryOptions MemoryCacheEntryOptions { get; private set; }

        public MemoryCacheOptions MemoryCacheOptions { get; private set; }

        public Cache()
            : this(new MemoryCacheOptions(), new MemoryCacheEntryOptions())
        {

        }

        public Cache(MemoryCacheOptions memoryCacheOptions)
            : this(memoryCacheOptions, new MemoryCacheEntryOptions())
        {
        }

        public Cache(MemoryCacheEntryOptions memoryCacheEntryOptions)
            : this(new MemoryCacheOptions(), memoryCacheEntryOptions)
        {

        }

        public Cache(MemoryCacheOptions memoryCacheOptions, MemoryCacheEntryOptions memoryCacheEntryOptions)
        {

            locks = new ConcurrentDictionary<object, SemaphoreSlim>();

            MemoryCacheOptions = memoryCacheOptions;
            MemoryCacheEntryOptions = memoryCacheEntryOptions;

        }

        public async Task<T> GetOrCreate(object key, Func<Task<T>> createItem)
        {

            cache ??= InitializeCache();

            if (cache.TryGetValue(key, out T entry))
            {
                return entry;
            }

            var localLock = locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
            await localLock.WaitAsync();

            try
            {
                if (!cache.TryGetValue(key, out entry))
                {
                    entry = await createItem();
                    var entryOptions = MemoryCacheEntryOptions ?? new MemoryCacheEntryOptions();
                    cache.Set(key, entry, entryOptions);

                }
            }
            finally
            {
                localLock.Release();
            }

            return entry;

        }

        public void Clear()
        {
            cache = null;
        }

        public void SetOption(MemoryCacheOptions options)
        {
            MemoryCacheOptions = options;
        }

        public void SetOption(MemoryCacheEntryOptions options)
        {
            MemoryCacheEntryOptions = options;
        }

        private MemoryCache InitializeCache()
        {
            return new MemoryCache(MemoryCacheOptions);
        }

    }
}
