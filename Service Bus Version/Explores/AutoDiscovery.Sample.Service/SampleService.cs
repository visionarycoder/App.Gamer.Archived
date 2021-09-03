using System;
using AutoDiscovery.Sample.Contract;

namespace AutoDiscovery.Sample.Service
{

	public class SampleService : ISampleService
	{

		public string GetData(int value)
		{
			return $"You entered: {value}";
		}

		public CompositeType GetDataUsingDataContract(CompositeType composite)
		{

			if (composite == null)
			{
				throw new ArgumentNullException(nameof(composite));
			}

			if (composite.BoolValue)
			{
				composite.StringValue += "Suffix";
			}
			return composite;

		}

	}

}