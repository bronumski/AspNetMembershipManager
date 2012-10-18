using System;

namespace AspNetMembershipManager.Logging
{
    public interface ILog
    {
        bool IsEnabled { get; }

        ILog Message(string message);
        ILog Message(string message, params object[] args);

        ILog Exception(Exception exception);
        ILog Exception(Exception exception, string message);
        ILog Exception(Exception exception, string message, params object[] args);
        
    }
}