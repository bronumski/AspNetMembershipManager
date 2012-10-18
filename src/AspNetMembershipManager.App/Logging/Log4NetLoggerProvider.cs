using System;

namespace AspNetMembershipManager.Logging
{
    public class Log4NetLoggerProvider : ILoggerProvider
    {
        public ILogger GetLogger(Type type)
        {
            return new Log4NetLogger(type);
        }
    }
}