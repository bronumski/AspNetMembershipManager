using System;
using System.Collections.Generic;
using log4net.Appender;
using log4net.Core;

namespace AspNetMembershipManager.Logging
{
    public class DelegateAppender : AppenderSkeleton
    {
		private static readonly List<DelegateAppender> delegateAppenders = new List<DelegateAppender>();
		public static IEnumerable<DelegateAppender> DelegateAppenders { get { return delegateAppenders; } }

    	public DelegateAppender()
    	{
			delegateAppenders.Add(this);
    	}

		~DelegateAppender()
		{
			delegateAppenders.Remove(this);
		}

		public event EventHandler<EventLoggedEventArgs> RaiseEventLoggedEvent;

		protected virtual void OnRaiseEventLogedEvent(EventLoggedEventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of 
            // a race condition if the last subscriber unsubscribes 
            // immediately after the null check and before the event is raised.
            EventHandler<EventLoggedEventArgs> handler = RaiseEventLoggedEvent;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
			OnRaiseEventLogedEvent(
				new EventLoggedEventArgs(loggingEvent, RenderLoggingEvent(loggingEvent)));
        } 
    }
}