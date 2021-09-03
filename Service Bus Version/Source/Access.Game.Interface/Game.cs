using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Gamer.Access.Game.Interface
{

	[DataContract]
	public class Game
	{

		[Key]
		[DataMember]
		public Guid GameId { get; set; } = Guid.NewGuid();

		[DataMember]
		public virtual Guid[] PlayerIds { get; set; } = new Guid[0];

		[DataMember]
		public Guid? CurrentPlayerId { get; set; }

	}


}
