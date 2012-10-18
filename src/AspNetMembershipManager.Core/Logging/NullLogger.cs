namespace AspNetMembershipManager.Logging
{
    public class NullLogger : ILogger
    {
        private readonly ILog log = new NullLog();


        public ILog Debug
        {
            get { return log; }
        }

        public ILog Trace
        {
            get { return log; }
        }

        public ILog Info
        {
            get { return log; }
        }

        public ILog Warn
        {
            get { return log; }
        }

        public ILog Error
        {
            get { return log; }
        }

        public ILog Fatal
        {
            get { return log; }
        }
    }
}