using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Gamer.Access.Player.Interface
{

	[DataContract]
	public class Player
	{

		public const string UNDEFINED_PLAYER_NAME = "Undefined";

		[Key]
		[DataMember]
		public Guid PlayerId { get; private set; } = Guid.NewGuid();

		[DataMember]
		public Guid GameId { get; set; } = Guid.Empty;

		[DataMember]
		public string PlayerName { get; set; } = UNDEFINED_PLAYER_NAME;

		[DataMember]
		public bool IsMachine { get; set; }

		[DataMember]
		public string GamePiece { get; set; }

	}

}
