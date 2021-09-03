using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ServiceModelEx;

namespace AutoDiscovery.Sample.Service
{

	class Host
	{

		public static void Main()
		{

			var baseAddress = DiscoveryHelper.AvailableTcpBaseAddress;
			var host = new ServiceHost(typeof(SampleService), baseAddress);
			host.AddDefaultEndpoints();
			host.Open();

			Console.WriteLine("Press ENTER to shut down service.");
			Console.ReadLine();

			host.Close();

		}

	}

}