using System.Runtime.Serialization;

namespace Gamer.Framework.Messaging
{

	[DataContract]
	public class MessageRequest : PubSubMessageBase
	{

		[DataMember]
		public string Message { get; set; }

	}
}
