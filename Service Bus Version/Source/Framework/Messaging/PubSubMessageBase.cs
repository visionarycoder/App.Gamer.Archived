using System;
using System.Runtime.Serialization;

namespace Gamer.Framework.Messaging
{

	[DataContract]
	public class PubSubMessageBase
	{

		[DataMember]
		public Guid Id { get; set; }

		[DataMember]
		public Guid CorrelationId { get; set; }

		[DataMember]
		public DateTime Timestamp { get; set; }

	}

}
