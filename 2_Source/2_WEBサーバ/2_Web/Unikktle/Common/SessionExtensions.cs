using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unikktle.Common
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public static int? GetIntAndRemove(this ISession session, string key)
        {
            var mode = session.GetInt32(key);
            session.Remove(key);

            return mode;
        }

        public static string GetStringAndRemove(this ISession session, string key)
        {
            var mode = session.GetString(key);
            session.Remove(key);

            return mode;
        }

        public static T GetAndRemove<T>(this ISession session, string key)
        {
            T mode = session.Get<T>(key);
            session.Remove(key);

            return mode;
        }
    }
}
