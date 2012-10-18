using System;

namespace AspNetMembershipManager.Logging
{
    internal class NullLog : ILog
    {
        public bool IsEnabled
        {
            get { return false; }
        }

        public ILog Message(string message)
        {
            return this;
        }

        public ILog Message(string message, params object[] args)
        {
            return this;
        }

        public ILog Exception(Exception exception)
        {
            return this;
        }

        public ILog Exception(Exception exception, string message)
        {
            return this;
        }

        public ILog Exception(Exception exception, string message, params object[] args)
        {
            return this;
        }
    }
}