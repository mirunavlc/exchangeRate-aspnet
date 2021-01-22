using ExchangeRate.Connectors;
using ExchangeRate.Helpers;
using ExchangeRate.JSONHandlers;
using ExchangeRate.Printers.Factory;
using ExchangeRate.SourcesConfiguration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRate
{
    class Program
    {
        private static readonly ILogger _logger = new Logger("Program");

        static async Task Main(string[] args)
        {
            var printModeStr = ConfigurationManager.AppSettings["PrintMode"];
            PrintModes printMode;
            bool success = Enum.TryParse(printModeStr, out printMode);

            if (!success)
            {
                _logger.
                    Error("Method <Main> App.config value={0} for PrintModes in not recognized.", printModeStr);
                return;
            }

            var sourcesConfig = (SourcesConfig)ConfigurationManager.GetSection("Sources");

            var tasks = new Dictionary<string, Task<Tuple<System.Net.HttpStatusCode, string>>>();
            foreach (SourcesInstanceElement instance in sourcesConfig.SourcesInstances)
            {
                var http = new HttpConnector(maxAttemptsToConnect: 1);
                tasks.Add(instance.SearchedProperty, http.Get(instance.Link));
            }

            var requestResponses = new Dictionary<string, string>();
            foreach (var task in tasks)
            {
                (var resultCode, var response) = await task.Value;

                if (resultCode != System.Net.HttpStatusCode.OK)
                {
                    _logger.
                         Warn("Method <Main> HttpRequest for property={0} was unresponsive. It will not be considered in the final comparison.", task.Key);
                    continue;
                }
                requestResponses.Add(task.Key, response);
            }

            var coinValues = new Dictionary<string, decimal>();
            foreach (var request in requestResponses)
            {
                Exception ex = null;
                var coinValue = Deserializer.ExtractProperty<decimal>(request.Value, request.Key, out ex);

                if (ex != null)
                {
                    _logger.
                         Warn("Method <Main> App.config key={0} not found. It will not be considered in the final comparison.", request.Key);
                    continue;
                }

                coinValues.Add(request.Key, coinValue);
            }

            var minValueForCoin = coinValues.Min(KeyValuePair => KeyValuePair.Value);
            Printer.InitializeFactories().ExecuteCreation(printMode).Print(minValueForCoin.ToString());
            _logger.Trace("Program completed.");
        }
    }
}
