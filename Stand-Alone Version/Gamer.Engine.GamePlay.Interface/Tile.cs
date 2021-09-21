using System;

namespace Gamer.Engine.GamePlay.Interface
{

	public class Tile
	{

		public Guid Id { get; init; }
		public Guid GameSessionId { get; init; }
		public string Address { get; init; }
		public Guid PlayerId { get; set; } = Guid.Empty;

		public bool IsEmpty => PlayerId == Guid.Empty;

	}

}