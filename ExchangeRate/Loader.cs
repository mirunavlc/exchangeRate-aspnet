using ExchangeRate.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace ExchangeRate
{
    public class Loader
    {
        private static readonly ILogger _logger = new Logger("Loader");

        public static T ExtractProperty<T>(string json, string propertyName)
        {
            T actual = default(T);
            try
            {
                var root = JToken.Parse(json);
                var tokens = root.SelectTokens(".." + propertyName);
                var token = tokens.Any() ? tokens.First() : null;
                if (token != null)
                {
                    actual = token.ToObject<T>();
                }
            }
            catch(Exception ex)
            {
                _logger.Warn(ex.Message + "Method <ExtractProperty> found no property={0} in json={1}.", propertyName, json);
            }
            return actual;
        }
    }
}
