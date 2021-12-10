using Microsoft.Extensions.Logging;

using System;
using System.Diagnostics;

namespace Gamer.Utility.Logging
{

    /// <summary>
    /// An adapter to leverage if your preferred logging solution doesn't support the ILogger abstraction
    /// </summary>
    public class CustomLogger : ILogger
    {

        private LogSink logger = new LogSink();

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            logger.Log<TState>(logLevel, eventId, exception.Message);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return default;
        }

    }

    /// <summary>
    /// You can use any logging implementation in place of this simple one.
    /// </summary>
    internal class LogSink
    {
        public void Log<T>(LogLevel logLevel, EventId eventId, string entry)
        {
            Debug.WriteLine($"{DateTime.UtcNow:G}: {typeof(T)}, {logLevel}, {eventId}, {entry}");
        }
    }
}