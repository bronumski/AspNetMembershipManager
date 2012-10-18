using System;

namespace AspNetMembershipManager.Logging
{
    public class NullLoggerProvider : ILoggerProvider
    {
        public ILogger GetLogger(Type type)
        {
            return new NullLogger();
        }
    }
}