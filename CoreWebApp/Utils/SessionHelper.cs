using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CoreWebApp.Utils
{
    public static class SessionHelper
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
        
        public static bool HasKey(this ISession session, string key)
        {
            return session.GetString(key) != null;
        }
    }
}
