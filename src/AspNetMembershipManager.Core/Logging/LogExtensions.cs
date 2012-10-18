using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNetMembershipManager.Logging
{
    public static class LogExtensions
    {
        public static ILogger Log<TLoggable>(this TLoggable loggableObject)
        {
            return LoggerProvider.GetLogger( typeof(TLoggable) );
        }

        public static ILog Message(this ILog log, params Func<string>[] messageFunctions)
    	{
    		TryLogMessages(
    			() => log.IsEnabled,
    			messageFunction => log.Message(messageFunction()),
    			messageFunctions);

            return log;
    	}

        public static ILog Message(this ILog log, string messageFormat, params Func<object>[] parameterFunctions)
        {
            TryLogMessages(
                () => log.IsEnabled,
                messageFunction => log.Message(messageFunction),
                new Func<string>[] { () => messageFormat.Format(ResolveMessageFunctions(parameterFunctions).ToArray()) });

            return log;
        }

        public static ILog Exception(this ILog log, Exception ex, Func<string> messageFunction)
    	{
    		TryLogMessages(
                () => log.IsEnabled, m => log.Exception(ex, m.Invoke()), new[] { messageFunction });

            return log;
    	}

        public static ILog Exception(this ILog log, Exception ex, string messageFormat, params Func<object>[] parameterFunctions)
        {
            TryLogMessages(
                () => log.IsEnabled,
                messageFunction => log.Exception(ex, messageFunction),
                new Func<string>[] { () => messageFormat.Format(ResolveMessageFunctions(parameterFunctions).ToArray()) });

            return log;
        }


        private static IEnumerable<object> ResolveMessageFunctions(IEnumerable<Func<object>> messageFunctions)
        {
            foreach (var messageFunction in messageFunctions)
            {
                object message;
                try
                {
                    message = messageFunction();
                }
                catch (Exception failToLogMessageException)
                {
                	message = "<Unable to resolve parameter>";
                	TryAndLogLoggerWarning(failToLogMessageException);
                }

            	yield return message;
            }
        }

    	private static void TryLogMessages(Func<bool> isLevelEnabled, Action<Func<string>> logMessage, IEnumerable<Func<string>> messageFunctions)
        {
            if(isLevelEnabled.Invoke( ))
            {
                foreach(var messageFunction in messageFunctions)
                {
                    try
                    {
                        logMessage.Invoke(messageFunction);
                    }
                    catch(Exception failToLogMessageException)
                    {
						TryAndLogLoggerWarning(failToLogMessageException);
					}
                }
            }
        }

    	private static void TryAndLogLoggerWarning(Exception failToLogMessageException)
    	{
    		try
    		{
    			LoggerProvider.GetLogger(typeof (LogExtensions)).Warn.Exception(failToLogMessageException, "Failed to log message.");
    		}
    		catch (Exception loggingException)
    		{
    			Console.Error.WriteLine("Failed to log message.");
    			Console.Error.WriteLine(new AggregateException(failToLogMessageException, loggingException));
    		}
    	}
    }
}