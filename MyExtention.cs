using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace weather {
    static class MyExtensions {
        public static IEnumerable<T> CatMaybe<T>(this IEnumerable<T?> source) where T : struct {
            foreach (var data in source)
                if (data != null) yield return (T) data;
        }
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue defaultValue) =>
            dict.TryGetValue(key, out var ret) ? ret : defaultValue;
        public static T Call<S, T>(this S value, Func<S, T> f) => f(value);
    }
}
