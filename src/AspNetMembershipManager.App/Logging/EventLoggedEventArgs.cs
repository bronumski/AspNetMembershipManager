using System;
using log4net.Core;

namespace AspNetMembershipManager.Logging
{
	public class EventLoggedEventArgs : EventArgs
	{
		public EventLoggedEventArgs(LoggingEvent loggingEvent, string logmessage)
		{
			LoggingEvent = loggingEvent;
			Logmessage = logmessage;
		}

		public LoggingEvent LoggingEvent { get; private set; }
		public string Logmessage { get; private set; }
	}
}