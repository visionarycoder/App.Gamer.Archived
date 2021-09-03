using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Gamer.Access.Tile.Interface
{

	[DataContract]
	public class Tile
	{

		public const string EMPTY_GAMEPIECE = "";

		[Key]
		[DataMember]
		public Guid GameId { get; set; }

		[Key]
		[DataMember]
		public int Row { get; set; }

		[Key]
		[DataMember]
		public string Column { get; set; }

		[DataMember]
		public string GamePiece { get; set; } = EMPTY_GAMEPIECE;

	}

}
