

using NLog;

namespace LoggerService.Extensions
{
    public static class LoggerExtensions
    {
        public static void LogInfo(this ILogger logger,string message)
        {
            logger.Debug(message);
        }
    }
}
