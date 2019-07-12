using log4net;
using log4net.Config;
using log4net.Repository;
using System;
using System.IO;

namespace FileApi.Utility
{
    public static class LogUtility
    {
        static ILoggerRepository repository = LogManager.CreateRepository("CoreRepository");
        static readonly ILog _log = LogManager.GetLogger(repository.Name, "CoreLogger");

        static LogUtility()
        {
            XmlConfigurator.Configure(repository, new FileInfo("Config/log4net.config"));
        }

        /// <summary>
        /// debug级别
        /// </summary>
        /// <param name="message"></param>
        public static void Debug(string message)
        {
            _log.Debug(message);
        }


        /// <summary>
        /// Info级别
        /// </summary>
        /// <param name="message"></param>
        public static void Info(string message)
        {
            _log.Info(message);
        }

        /// <summary>
        /// warn级别
        /// </summary>
        /// <param name="message"></param>
        public static void Warn(string message)
        {
            _log.Warn(message);
        }

        /// <summary>
        /// error级别
        /// </summary>
        /// <param name="message"></param>
        public static void Error(string message)
        {
            _log.Error(message);
        }

        /// <summary>
        /// error级别
        /// </summary>
        /// <param name="message"></param>
        public static void Exception(Exception ex)
        {
            _log.Error(ex.Message);
        }

        /// <summary>
        /// Fatal级别
        /// </summary>
        /// <param name="message"></param>
        public static void Fatal(string message)
        {
            _log.Fatal(message);
        }
    }
}
