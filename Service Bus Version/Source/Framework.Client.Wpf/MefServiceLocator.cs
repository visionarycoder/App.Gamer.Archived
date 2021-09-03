using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using Microsoft.Practices.ServiceLocation;

namespace Gamer.Infrastructure.Client.Wpf
{

	public class MefServiceLocator : ServiceLocatorImplBase
	{

		private readonly ExportProvider provider;

		public MefServiceLocator(ExportProvider provider)
		{
			this.provider = provider;
		}

		protected override object DoGetInstance(Type serviceType, string key)
		{
			if (key == null)
				key = AttributedModelServices.GetContractName(serviceType);

			var exports = provider.GetExports<object>(key).ToList<Lazy<object>>();
			if (exports.Any())
				return exports.First().Value;

			throw new ActivationException($"Could not locate any instances of contract {key}");
		}

		protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
		{
			var exports = provider.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
			return exports;
		}

		public static void InjectInto(CompositionContainer container)
		{
			var mefServiceLocator = new MefServiceLocator(container);
			ServiceLocator.SetLocatorProvider(() => mefServiceLocator);
		}

	}

}