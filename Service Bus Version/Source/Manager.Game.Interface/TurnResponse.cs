using System.Runtime.Serialization;
using Gamer.Framework.Messaging;

namespace Gamer.Manager.Game.Interface
{

	[DataContract]
	public class TurnResponse : PubSubMessageBase
	{

		[DataMember]
		public TurnRequest TurnRequest { get; set; }

	}

}
