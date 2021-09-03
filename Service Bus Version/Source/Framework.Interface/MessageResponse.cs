using System.Runtime.Serialization;

namespace Gamer.Infrastructure.Contract
{

	[DataContract]
	public class MessageResponse : PubSubMessageBase
	{

		[DataMember]
		public string Message { get; set; }

	}

}