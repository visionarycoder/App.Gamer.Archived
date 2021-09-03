using System;
using System.Runtime.Serialization;

namespace Gamer.Manager.Turn.Interface
{

	[DataContract]
	public class TurnRequest : RequestBase
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
