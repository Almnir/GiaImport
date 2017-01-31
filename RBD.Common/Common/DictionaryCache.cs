using System;
using System.Collections.Generic;

namespace RBD.Common.Common
{
    public class DictionaryCache<TKey, TValue> : ICache<TKey, TValue>
    {
        readonly Dictionary<TKey, TValue> _cache = new Dictionary<TKey, TValue>();

        public TValue Get(TKey key)
        {
            return _cache.ContainsKey(key) ? _cache[key] : default(TValue);
        }

        public TValue Insert(TKey key, TValue value)
        {
            _cache[key] = value; 
            return value;
        }
    }
}
