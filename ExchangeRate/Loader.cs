using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;

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
