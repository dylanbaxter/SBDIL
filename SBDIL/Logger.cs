using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBDIL
{
    public static class Logger
    {
        private static bool isConfigured = false;
        private static ILog iLog;

        public static void Configure()
        {
            if (isConfigured)
                return;

            var loggerName = "SBDIL";

            var logger = (log4net.Repository.Hierarchy.Logger)log4net.LogManager.GetRepository().GetLogger(loggerName);
            var ilogger = log4net.LogManager.GetRepository().GetLogger(loggerName);

            //Add the default log appender if none exist
            if(logger.Appenders.Count == 0)
            {
                var directoryName = "C:\\BarkLogs";

                //If the directory doesn't exist then create it
                if(!Directory.Exists(directoryName))
                    Directory.CreateDirectory(directoryName);

                var fileName = Path.Combine(directoryName, DateTime.Now.ToString("yyyyMMddTHHmmss") + ".txt");

                //Create the rolling file appender
                var appender = new log4net.Appender.RollingFileAppender();
                appender.Name = "RollingFileAppender";
                appender.File = fileName;
                appender.StaticLogFileName = true;
                appender.AppendToFile = false;
                appender.RollingStyle = log4net.Appender.RollingFileAppender.RollingMode.Size;
                appender.MaxSizeRollBackups = 10;
                appender.MaximumFileSize = "10MB";
                appender.PreserveLogFileNameExtension = true;

                //Configure the layout of the trace message write
                var layout = new log4net.Layout.PatternLayout()
                {
                    ConversionPattern = "%date{MM/dd/yyyy hh:mm:ss.fff} - %message%newline"
                };
                appender.Layout = layout;
                layout.ActivateOptions();

                //Let log4net configure itself based on the values provided
                appender.ActivateOptions();
                log4net.Config.BasicConfigurator.Configure(appender);
            }

            iLog = LogManager.GetLogger(loggerName);
            isConfigured = true;
        }

        public static event MessageLoggedDelegate OnExceptionLogged;

        public static void Debug(object message) { Configure(); iLog.Debug(message); OnExceptionLogged(new MessageLoggedEventArgs(message.ToString())); }
        public static void Debug(object message, Exception exception) { Configure(); iLog.Debug(message, exception); OnExceptionLogged(new MessageLoggedEventArgs(message.ToString())); }

        public static void Error(object message) { Configure(); iLog.Error(message); OnExceptionLogged(new MessageLoggedEventArgs(message.ToString())); }
        public static void Error(object message, Exception exception) { Configure(); iLog.Error(message, exception); OnExceptionLogged(new MessageLoggedEventArgs(message.ToString())); }

        public static void Fatal(object message) { Configure(); iLog.Fatal(message); OnExceptionLogged(new MessageLoggedEventArgs(message.ToString())); }
        public static void Fatal(object message, Exception exception) { Configure(); iLog.Fatal(message, exception); OnExceptionLogged(new MessageLoggedEventArgs(message.ToString())); }

        public static void Info(object message) { Configure(); iLog.Info(message); OnExceptionLogged(new MessageLoggedEventArgs(message.ToString())); }
        public static void Info(object message, Exception exception) { Configure(); iLog.Info(message, exception); OnExceptionLogged(new MessageLoggedEventArgs(message.ToString())); }

        public static void Warn(object message) { Configure(); iLog.Warn(message); OnExceptionLogged(new MessageLoggedEventArgs(message.ToString())); }
        public static void Warn(object message, Exception exception) { Configure(); iLog.Warn(message, exception); OnExceptionLogged(new MessageLoggedEventArgs(message.ToString())); }
    }

    public class MessageLoggedEventArgs : EventArgs
    {
        private string _message;
        public MessageLoggedEventArgs(string message)
        {
            _message = message;
        }
        public string Message { get { return _message; } }
    }

    public delegate void MessageLoggedDelegate(MessageLoggedEventArgs args);
}
