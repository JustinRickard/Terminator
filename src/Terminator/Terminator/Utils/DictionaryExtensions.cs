using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminator.Utils
{
    public static class DictionaryExtensions
    {
        public static void TryAdd<TKey,TValue> (this Dictionary<TKey,TValue> dict, TKey key, TValue value) {
            if (!dict.ContainsKey(key))
            {
                dict.Add(key, value);
            }
        }
    }
}
