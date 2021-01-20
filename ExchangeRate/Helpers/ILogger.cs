using System;

namespace ExchangeRate.Helpers
{
    public interface ILogger
    {
        void Shutdown();

        void Exception(string method = null, Exception exception = null, string message = null, params object[] args);
        void Exception(Exception exception = null, string message = null, params object[] args);

        void Error(string message = null, params object[] args);
        void Error(string method, string message = null, params object[] args);

        void Fatal(string message = null, params object[] args);
        void Fatal(string method, string message = null, params object[] args);

        void Warn(string message = null, params object[] args);
        void Warn(string method, string message = null, params object[] args);

        void Info(string message = null, params object[] args);
        void Info(string method, string message = null, params object[] args);

        void Debug(string message = null, params object[] args);
        void Debug(string method, string message = null, params object[] args);

        void Trace(string message = null, params object[] args);
        void Trace(string method, string message = null, params object[] args);
    }
}
