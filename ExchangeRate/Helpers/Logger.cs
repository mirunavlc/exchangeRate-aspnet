using NLog;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Web;
using System.Web.SessionState;

namespace MdtDebtManagerIntegration
{
    public class Logger : ILogger
    {

        #region Datamembers
        private static NLog.Logger _log = null;
        #endregion

        #region Class Initializer

        private string _logSection;
        public Logger(string logSection)
        {
            _log = LogManager.GetLogger("App");
            _logSection = logSection;
        }
        public Logger(string logName, string logSection)
        {
            _log = LogManager.GetLogger(string.IsNullOrEmpty(logName) ? "App" : logName.Trim());
            _logSection = logSection;
        }

        public void Shutdown()
        {
            try
            {
                //NLog.LogManager.Shutdown();
                NLog.LogManager.Configuration = null;
            }
            catch (Exception)
            {

            }
        }

        private void Log(LogLevel level, string method = null, string message = null, params object[] args)
        {
            Log(level, method, null, message, args);
        }

        private void Log(LogLevel level, string method = null, Exception exception = null, string message = null, params object[] args)
        {
            if (exception != null && !_log.IsErrorEnabled) return;

            var theEvent = new LogEventInfo(level, _log.Name, CultureInfo.CurrentUICulture, message, args, exception);

            method = string.Format("{0}.{1}", string.IsNullOrWhiteSpace(_logSection) ? "" : _logSection, method);

            theEvent.Properties["method"] = method;
            theEvent.Properties["callsite"] = method;

            if (HttpContext.Current != null)
            {
                HttpContext context = HttpContext.Current;
                if (context.Handler is IRequiresSessionState && context.Session != null)
                {
                    theEvent.Properties["SessionId"] = context.Session.SessionID.PadRight(24, ' ');
                }
                else
                {
                    //theEvent.Properties["SessionId"] = "-----OUT-OF-SESSION-----".PadRight(24, ' ');
                }

                if (context.User != null && !string.IsNullOrEmpty(context.User.Identity.Name))
                {
                    theEvent.Properties["WebUser"] = context.User.Identity.Name;
                }
                else
                {
                    theEvent.Properties["WebUser"] = "Anonymous".PadRight(16, ' ');
                }

                if (context.Handler != null)
                {
                    if (context.Request != null)
                    {
                        theEvent.Properties["document-uri"] =
                        theEvent.Properties["RequestUrl"] = context.Request.Url.ToString();
                    }
                    else
                    {
                        theEvent.Properties["RequestUrl"] = "".PadRight(16, ' ');
                    }
                }
            }

            _log.Log(theEvent);
            return;
        }

        public void SessionLog(string session, string username, string message = null)
        {
            SessionLog(LogLevel.Trace, session, username, message);
        }

        public void SessionLog(LogLevel level, string session, string username, string message = null, params object[] args)
        {
            if (string.IsNullOrWhiteSpace(session)) return;
            var logEvent = new LogEventInfo(level, _log.Name, CultureInfo.CurrentUICulture, message, args);
            logEvent.Properties["session"] = session;
            logEvent.Properties["user"] = string.IsNullOrWhiteSpace(username) ? "?" : username;
            _log.Log(logEvent);
        }

        private string GetCallingMethodName(MethodBase currentMethod)
        {
            string methodName = string.Empty;
            try
            {
                StackTrace sTrace = new StackTrace(true);
                for (int frameCount = 0; frameCount < sTrace.FrameCount; frameCount++)
                {
                    StackFrame sFrame = sTrace.GetFrame(frameCount);
                    MethodBase thisMethod = sFrame.GetMethod();
                    if (thisMethod == currentMethod)
                    {
                        if (frameCount + 1 <= sTrace.FrameCount)
                        {
                            StackFrame prevFrame = sTrace.GetFrame(frameCount + 1);
                            MethodBase prevMethod = prevFrame.GetMethod();
                            methodName = prevMethod.ReflectedType + "." + prevMethod.Name;
                        }
                        break;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            return methodName;
        }

        #endregion

        #region LoggerMembers

        public void Exception(string method = null, Exception exception = null, string message = null, params object[] args)
        {
            if (_log.IsErrorEnabled)
            {
                if (string.IsNullOrEmpty(method))
                {
                    method = GetCallingMethodName(MethodBase.GetCurrentMethod());
                }
                Log(LogLevel.Error, method, exception, message, args);
            }
        }

        public void Exception(Exception exception = null, string message = null, params object[] args)
        {
            if (_log.IsErrorEnabled)
            {
                string method = GetCallingMethodName(MethodBase.GetCurrentMethod());
                Log(LogLevel.Error, method, exception, message, args);
            }
        }

        public void Error(string message = null, params object[] args)
        {
            if (_log.IsErrorEnabled)
            {
                string method = GetCallingMethodName(MethodBase.GetCurrentMethod());
                Log(LogLevel.Error, method, null, message, args);
            }
        }

        public void Error(string method = null, string message = null, params object[] args)
        {
            if (_log.IsErrorEnabled)
            {
                if (string.IsNullOrEmpty(method))
                {
                    method = GetCallingMethodName(MethodBase.GetCurrentMethod());
                }
                Log(LogLevel.Error, method, null, message, args);
            }
        }

        public void Fatal(string message = null, params object[] args)
        {
            if (_log.IsErrorEnabled)
            {
                string method = GetCallingMethodName(MethodBase.GetCurrentMethod());
                Log(LogLevel.Fatal, method, null, message, args);
            }
        }

        public void Fatal(string method = null, string message = null, params object[] args)
        {
            if (_log.IsErrorEnabled)
            {
                if (string.IsNullOrEmpty(method))
                {
                    method = GetCallingMethodName(MethodBase.GetCurrentMethod());
                }
                Log(LogLevel.Fatal, method, null, message, args);
            }
        }

        public void Warn(string message = null, params object[] args)
        {
            if (_log.IsWarnEnabled)
            {
                string method = GetCallingMethodName(MethodBase.GetCurrentMethod());
                Log(LogLevel.Warn, method, null, message, args);
            }
        }

        public void Warn(string method = null, string message = null, params object[] args)
        {
            if (_log.IsWarnEnabled)
            {
                if (string.IsNullOrEmpty(method))
                {
                    method = GetCallingMethodName(MethodBase.GetCurrentMethod());
                }
                Log(LogLevel.Warn, method, null, message, args);
            }
        }

        public void Info(string message = null, params object[] args)
        {
            if (_log.IsInfoEnabled)
            {
                string method = GetCallingMethodName(MethodBase.GetCurrentMethod());
                Log(LogLevel.Info, method, null, message, args);
            }
        }

        public void Info(string method = null, string message = null, params object[] args)
        {
            if (_log.IsInfoEnabled)
            {
                if (string.IsNullOrEmpty(method))
                {
                    method = GetCallingMethodName(MethodBase.GetCurrentMethod());
                }
                Log(LogLevel.Info, method, null, message, args);
            }
        }

        public void Debug(string message = null, params object[] args)
        {
            if (_log.IsDebugEnabled)
            {
                string method = GetCallingMethodName(MethodBase.GetCurrentMethod());
                Log(LogLevel.Debug, method, null, message, args);
            }
        }

        public void Debug(string method = null, string message = null, params object[] args)
        {
            if (_log.IsDebugEnabled)
            {
                if (string.IsNullOrEmpty(method))
                {
                    method = GetCallingMethodName(MethodBase.GetCurrentMethod());
                }
                Log(LogLevel.Debug, method, null, message, args);
            }
        }

        public void Trace(string message = null, params object[] args)
        {
            if (_log.IsDebugEnabled)
            {
                string method = GetCallingMethodName(MethodBase.GetCurrentMethod());
                Log(LogLevel.Trace, method, null, message, args);
            }
        }

        public void Trace(string method = null, string message = null, params object[] args)
        {
            if (_log.IsDebugEnabled)
            {
                if (string.IsNullOrEmpty(method))
                {
                    method = GetCallingMethodName(MethodBase.GetCurrentMethod());
                }
                Log(LogLevel.Trace, method, null, message, args);
            }
        }

        #endregion
    }
}
