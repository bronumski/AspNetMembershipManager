using System;

namespace AspNetMembershipManager.Logging
{
    public interface ILoggerProvider
    {
        ILogger GetLogger( Type type );
    }
}