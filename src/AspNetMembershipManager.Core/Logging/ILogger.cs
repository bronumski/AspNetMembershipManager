namespace AspNetMembershipManager.Logging
{
    /// <summary>
    /// Defines methods for logging messages.
    /// </summary>
    public interface ILogger
    {
        ILog Debug { get; }
        ILog Trace { get; }
        ILog Info { get; }
        ILog Warn { get; }
        ILog Error { get; }
        ILog Fatal { get; }
    }

}