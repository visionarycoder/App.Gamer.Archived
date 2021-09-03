using System;
using System.Runtime.Serialization;

namespace Gamer.Access.Player.Interface
{

	[DataContract]
	public class PlayerFilter
	{

		[DataMember]
		public Guid GameId { get; set; }

		[DataMember]
		public Guid PlayerId { get; set; }

		[DataMember]
		public int? PlayerNumber { get; set; }

		[DataMember]
		public bool? CurrentPlayer { get; set; }

		[DataMember]
		public bool? GetAllPlayers { get; set; }

	}

}