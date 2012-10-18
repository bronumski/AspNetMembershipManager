using System;

namespace AspNetMembershipManager.Logging
{
	public static class LoggerProvider
	{
		public static void ResetProvider()
		{
            Provider = new NullLoggerProvider();
        }

		public static ILoggerProvider Provider { get; set; }

		public static ILogger GetLogger(Type type)
		{
			if (Provider == null)
			{
			    ResetProvider();
			}
			return Provider.GetLogger(type);
		}
	}
}