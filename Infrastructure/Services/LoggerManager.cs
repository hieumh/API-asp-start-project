using API_asp_start_project.Infrastructure.Services;
using NLog;

namespace API_asp_start_project.Infrastructure.Services
{
    public class LoggerManager: ILoggerManager
    {
        private static NLog.ILogger logger = LogManager.GetCurrentClassLogger();

        public void LogInfo(string message) => logger.Info(message);
        public void LogDebug(string message) => logger.Debug(message);
        public void LogWarn(string message) => logger.Warn(message);
        public void LogError(string message) => logger.Error(message);
    }
}
