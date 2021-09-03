using System.Runtime.Serialization;

namespace Gamer.Infrastructure.Contract
{

	[DataContract]
	public class MessageRequest : PubSubMessageBase
	{

		[DataMember]
		public string Message { get; set; }

	}
}
