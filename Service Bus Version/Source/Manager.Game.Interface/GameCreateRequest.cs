using System;
using System.Runtime.Serialization;
using Gamer.Framework.Messaging;

namespace Gamer.Manager.Game.Interface
{

	[DataContract]
	public class GameCreateRequest : PubSubMessageBase
	{

		[DataMember]
		public Guid GameId { get; set; } = Guid.NewGuid();

		[DataMember]
		public int NumberOfPlayers { get; set; }


	}

}
