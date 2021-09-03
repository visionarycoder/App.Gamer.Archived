using System.Runtime.Serialization;
using System.ServiceModel;
using ServiceModelEx.ServiceFabric.Services.Remoting;

namespace AutoDiscovery.Sample.Contract
{

	[ServiceContract]
	public interface ISampleService : IService
	{

		[OperationContract]
		string GetData(int value);

		[OperationContract]
		CompositeType GetDataUsingDataContract(CompositeType composite);

	}

	[DataContract]
	public class CompositeType
	{


		[DataMember]
		public bool BoolValue { get; set; }

		[DataMember]
		public string StringValue { get; set; }

	}

}