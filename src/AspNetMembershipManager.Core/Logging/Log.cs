using System;

namespace AspNetMembershipManager.Logging
{
    public class Log : ILog
    {
        private readonly Func<bool> enabledFunc;
        private readonly Action<string, object[]> messageAction;
        private readonly Action<Exception, string, object[]> exceptionAction;


        public Log(Func<bool> enabledFunc, Action<string, object[]> messageAction, Action<Exception, string, object[]> exceptionAction)
        {
            this.enabledFunc = enabledFunc;
            this.messageAction = messageAction;
            this.exceptionAction = exceptionAction;
        }

        public bool IsEnabled
        {
            get { return enabledFunc(); }
        }

        public ILog Message(string message)
        {
            if (enabledFunc())
            {
                messageAction(message, new object[0]);
            }

            return this;
        }

        public ILog Message(string message, params object[] args)
        {
            if (enabledFunc())
            {
                messageAction(message, args);
            }
            return this;
        }


        public ILog Exception(Exception exception)
        {
            if (enabledFunc())
            {
                exceptionAction(exception, string.Empty, new object[0]);
            }
            return this;
        }

        public ILog Exception(Exception exception, string message)
        {
            if (enabledFunc())
            {
                exceptionAction(exception, message, new object[0]);
            }
            return this;
        }

        public ILog Exception(Exception exception, string message, params object[] args)
        {
            if (enabledFunc())
            {
                exceptionAction(exception, message, args);
            }
            return this;
        }

    }
}