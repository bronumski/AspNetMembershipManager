using System;
using log4net;
using log4net.Core;

namespace AspNetMembershipManager.Logging
{
	public class Log4NetLogger : ILogger
	{
		private readonly Log trace;
		private readonly Log debug;
		private readonly Log info;
		private readonly Log warn;
		private readonly Log error;
		private readonly Log fatal;


		public Log4NetLogger(Type type)
		{
			var log4netLogger = LogManager.GetLogger(type);

			trace = new Log(() => log4netLogger.Logger.IsEnabledFor(Level.Trace), (messageFormat, parameters) => LogMessage(log4netLogger, Level.Trace, messageFormat, parameters), (exception, messageFormat, parameters) => LogException(log4netLogger, Level.Trace, exception, messageFormat, parameters));
			debug = new Log(() => log4netLogger.IsDebugEnabled, (messageFormat, parameters) => LogMessage(log4netLogger, Level.Debug, messageFormat, parameters), (exception, messageFormat, parameters) => LogException(log4netLogger, Level.Debug, exception, messageFormat, parameters));
			info = new Log(() => log4netLogger.IsInfoEnabled, (messageFormat, parameters) => LogMessage(log4netLogger, Level.Info, messageFormat, parameters), (exception, messageFormat, parameters) => LogException(log4netLogger, Level.Info, exception, messageFormat, parameters));
			warn = new Log(() => log4netLogger.IsWarnEnabled, (messageFormat, parameters) => LogMessage(log4netLogger, Level.Warn, messageFormat, parameters), (exception, messageFormat, parameters) => LogException(log4netLogger, Level.Warn, exception, messageFormat, parameters));
			error = new Log(() => log4netLogger.IsErrorEnabled, (messageFormat, parameters) => LogMessage(log4netLogger, Level.Error, messageFormat, parameters), (exception, messageFormat, parameters) => LogException(log4netLogger, Level.Error, exception, messageFormat, parameters));
			fatal = new Log(() => log4netLogger.IsFatalEnabled, (messageFormat, parameters) => LogMessage(log4netLogger, Level.Fatal, messageFormat, parameters), (exception, messageFormat, parameters) => LogException(log4netLogger, Level.Fatal, exception, messageFormat, parameters));
		}

		private void LogMessage(log4net.ILog log, Level level, string message, params object[] args)
		{
			try
			{
				if (args.Length == 0)
				{
					log.Logger.Log(typeof(LogExtensions), level, message, null);
				}
				else
				{
					log.Logger.Log(typeof(LogExtensions), level, message.Format(args), null);
				}
			}
			catch (Exception ex)
			{
                LogManager.GetLogger(GetType()).Warn("Failed to log '{0}' message.".Format(level), ex);
			}
		}

		private void LogException(log4net.ILog log, Level level, Exception exception, string message, params object[] args)
		{
			try
			{
				if (args.Length == 0)
				{
					log.Logger.Log(typeof(LogExtensions), level, message, exception);
				}
				else
				{
                    log.Logger.Log(typeof(LogExtensions), level, message.Format(args), exception);
				}
			}
			catch (Exception ex)
			{
                LogManager.GetLogger(GetType()).Warn("Failed to log '{0}' message.".Format(level), ex);

				log.Logger.Log(typeof(LogExtensions), level, string.Empty, exception);
			}
		}

		public ILog Trace
		{
			get { return trace; }
		}


		public ILog Debug
		{
			get { return debug; }
		}


		public ILog Info
		{
			get { return info; }
		}


		public ILog Warn
		{
			get { return warn; }
		}


		public ILog Error
		{
			get { return error; }
		}


		public ILog Fatal
		{
			get { return fatal; }
		}
	}
}