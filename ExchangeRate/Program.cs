using ExchangeRate.Connectors;
using ExchangeRate.Helpers;
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
        static async Task Main(string[] args)
        {
            var printModeStr = ConfigurationManager.AppSettings["PrintMode"];
            PrintModes printMode;
            bool success = Enum.TryParse(printModeStr, out printMode);

            if (!success)
            {
                new Logger("HttpConnector").
                    Error("Method <Main> App.config value={0} for PrintModes in not recognized.", printModeStr);
                return;
            }

            var sourcesConfig = (SourcesConfig)ConfigurationManager.GetSection("Sources");

            var tasks = new Dictionary<string, Task<Tuple<System.Net.HttpStatusCode, string>>>();
            foreach (SourcesInstanceElement instance in sourcesConfig.SourcesInstances)
            {
                var http = new HttpConnector(1);
                tasks.Add(instance.SearchedProperty, http.Get(instance.Link));
            }

            var requestResponses = new Dictionary<string, string>();
            foreach (var task in tasks)
            {
                (var resultCode, var response) = await task.Value;
                requestResponses.Add(task.Key, response);
            }

            var coinValues = new Dictionary<string, decimal>();
            foreach (var request in requestResponses)
            {
                coinValues.Add(request.Key, Loader.ExtractProperty<decimal>(request.Value, request.Key));
            }

            var minValueForCoin = coinValues.Min(KeyValuePair => KeyValuePair.Value);
            Printer.InitializeFactories().ExecuteCreation(printMode).Print(minValueForCoin.ToString());
        }
    }
}
