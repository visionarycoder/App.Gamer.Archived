using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using System.Text;
using System.Threading.Tasks;
using AutoDiscovery.Sample.Contract;
using AutoDiscovery.Sample.Service;
using ServiceModelEx;

namespace AutoDiscovery.Client
{

	class Program
	{


		public static void Main(string[] args)
		{


		}

		static EndpointAddress serviceAddress;

		static void Main()
		{
			if (FindService()) InvokeService();
		}

		// ** DISCOVERY ** //  
		static bool FindService()
		{

			Console.WriteLine("\nFinding Calculator Service ..");
			DiscoveryClient discoveryClient = new DiscoveryClient(new UdpDiscoveryEndpoint());

			Collection<EndpointDiscoveryMetadata> sampleServices = discoveryClient.Find(new FindCriteria(typeof(ISampleService)));

			discoveryClient.Close();

			if (sampleServices.Count == 0)
			{
				Console.WriteLine("\nNo services are found.");
				return false;
			}
			else
			{
				serviceAddress = sampleServices[0].EndpointAddress;
				return true;
			}
		}

		static void InvokeService()
		{
			Console.WriteLine("\nInvoking Calculator Service at {0}\n", serviceAddress);

			// Create a client  
			var client = new SampleService();
			client.Endpoint.Address = serviceAddress;
			client.Add(10, 3);
		}


	}

}
