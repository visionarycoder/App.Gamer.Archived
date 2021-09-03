using System;
using System.Runtime.Serialization;
using Gamer.Framework.Messaging;

namespace Gamer.Manager.Game.Interface
{

	[DataContract]
	public class TurnRequest : PubSubMessageBase
	{

		[DataMember]
		public Guid SessionId { get; set; }

		[DataMember]
		public Guid PlayerId { get; set; }

		[DataMember]
		public int Row { get; set; }

		[DataMember]
		public string Column { get; set; }

	}

}
