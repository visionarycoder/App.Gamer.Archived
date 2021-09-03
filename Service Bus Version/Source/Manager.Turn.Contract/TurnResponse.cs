using System.Runtime.Serialization;

namespace Gamer.Manager.Turn.Interface
{

	[DataContract]
	public class TurnResponse : DataCommitResponse
	{

		[DataMember]
		public TurnRequest TurnRequest { get; set; }

	}

}
