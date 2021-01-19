using Newtonsoft.Json.Linq;
using System.Linq;

namespace ExchangeRate
{
    public class Loader
    {
        public static T ExtractProperty<T>(string json, string propertyName)
        {
            var root = JToken.Parse(json);
            var tokens = root.SelectTokens(".." + propertyName);
            var token = tokens.Any() ? tokens.First() : null;
            T actual = default(T);
            if (token != null)
            {
                actual = token.ToObject<T>();
            }
            return actual;
        }
    }
}
