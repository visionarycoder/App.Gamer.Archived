using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Gamer.Framework
{

	public abstract class ServiceBase : IServiceBase
	{

		protected ILogger logger { get; init; }
		
		public Guid InstanceId { get; }  = Guid.NewGuid();

		public ServiceBase(ILogger logger)
		{
			this.logger = logger ?? NullLogger.Instance;
			var caller = ReflectionHelper.NameOfCallingClass();
			logger.LogInformation($"Starting {caller}");

		}

	}

}
