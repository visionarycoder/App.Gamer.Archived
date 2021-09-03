using System.Runtime.Serialization;

namespace Gamer.Util.PubSub.Interface
{

	[DataContract]
	public class DataCommitResponse : MessageResponse
	{

		[DataMember]
		public int? CommitCount { get; set; }

		[DataMember]
		public OperationStatus OperationStatus { get; set; }

		[DataMember]
		public bool HasError { get; set; }

		[DataMember]
		public string ErrorMessage { get; set; }

	}

}