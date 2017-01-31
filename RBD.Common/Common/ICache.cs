using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RBD.Common.Common
{
    public interface ICache<TKey, TValue>
    {
        TValue Get(TKey key);
        TValue Insert(TKey key, TValue value);
    }
}
