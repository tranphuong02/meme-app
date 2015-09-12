using System;
using log4net;

namespace Core.Logger
{
    public class Logger : ILogger
    {
        private readonly ILog _logger;

        public Logger()
        {
            _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Warn(Exception exception)
        {
            _logger.Warn(exception);
        }

        public void Warn(string message, Exception exception)
        {
            _logger.Warn(message, exception);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(Exception exception)
        {
            _logger.Error(exception.Message);
        }

        public void Error(string message, Exception exception)
        {
            _logger.Error(message, exception);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Fatal(Exception exception)
        {
            Fatal(exception.Message);
        }

        public void Fatal(string message, Exception exception)
        {
           // 
        }
    }
}
