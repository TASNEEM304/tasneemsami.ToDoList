using NLog;
using NLog.Extensions.Logging;
namespace TasneemSami.ToDoList.Api.Logger
{
    public class LoggerManger : ILoggerManger
    {
       
        private static NLog.ILogger logger = NLog.LogManager.GetCurrentClassLogger();

        public void LogDebug(string message)
        {
            logger.Debug(message);
        }

        public void LogError(string message)
        {
            logger.Error(message);
        }

        public void LogInfo(string message)
        {
            logger.Info(message);
        }

        public void LogWarn(string message)
        {
            logger.Warn(message);
        }
    }
}
