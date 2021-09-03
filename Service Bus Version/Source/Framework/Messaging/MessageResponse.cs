using System.Runtime.Serialization;

namespace Gamer.Framework.Messaging
{

	[DataContract]
	public class MessageResponse : PubSubMessageBase
	{

		[DataMember]
		public string Message { get; set; }

	}

}