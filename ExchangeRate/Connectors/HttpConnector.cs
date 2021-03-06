﻿using ExchangeRate.Helpers;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ExchangeRate.Connectors
{
    public class HttpConnector
    {
        private readonly ILogger _logger = new Logger("HttpConnector");

        private readonly uint _maxAttemptsToConnect;

        public HttpConnector(uint maxAttemptsToConnect) => _maxAttemptsToConnect = maxAttemptsToConnect;

        public async Task<Tuple<HttpStatusCode, string>> Get(string uri, int timeoutMs = -1, int retryCount = 0)
        {
            await Task.Delay(5000);
            if (retryCount > _maxAttemptsToConnect)
            {
                _logger.Warn("Method <Get> exceeded the maximum number of attepts to connect to {0}.", uri);
                return new Tuple<HttpStatusCode, string>(HttpStatusCode.ServiceUnavailable, null);
            }

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(uri);

                if (timeoutMs != -1)
                {
                    request.Timeout = timeoutMs;
                }

                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                using (var response = (HttpWebResponse)await request.GetResponseAsync())
                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    var finalResponse = await reader.ReadToEndAsync();
                    return new Tuple<HttpStatusCode, string>(response.StatusCode, finalResponse);
                }
            }
            catch (WebException we)
            {
                var resp = we.Response as HttpWebResponse;
                if (resp == null)
                {
                    _logger.Warn(we.Message + "Method <Get> attepts to connect to {0}.", uri);
                    return await Get(uri, timeoutMs, retryCount + 1);
                }
                return new Tuple<HttpStatusCode, string>(resp.StatusCode, null);
            }
            catch (Exception ex)
            {
                _logger.Warn(ex.Message + "Method <Get> attepts to connect to {0}.", uri);
                return new Tuple<HttpStatusCode, string>(HttpStatusCode.NotFound, null);
            }
        }
    }
}
