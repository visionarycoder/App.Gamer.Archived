using System;
using Microsoft.Extensions.Logging;

namespace Gamer.Framework.Logging
{

	public class TraceLogger : ILogger
	{
		
	  public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
	  {
	  }

	  public bool IsEnabled(LogLevel logLevel) => false;
	
	  public IDisposable BeginScope<TState>(TState state) => default;
	  
  }

	public class TraceLogger<T> : ILogger<T>
	{
	
		public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
		{
			throw new NotImplementedException();
		}

		public bool IsEnabled(LogLevel logLevel) => false;

		public IDisposable BeginScope<TState>(TState state) => default;

	}

}