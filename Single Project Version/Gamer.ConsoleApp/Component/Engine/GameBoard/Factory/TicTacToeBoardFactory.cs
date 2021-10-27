using System;
using System.Collections.Generic;
using Gamer.Component.Access.Tile;

namespace Gamer.Component.Engine.GameBoard
{
	
	public static class TicTacToeBoardFactory
	{

		public static IEnumerable<Tile> Create(Guid gameSessionId, string[] addresses)
		{

			var list = new List<Tile>();
			foreach (var address in addresses)
			{
				var tile = new Tile
				{
					Id = Guid.NewGuid(),
					Address = address,
					GameSessionId = gameSessionId,
					PlayerId = Guid.Empty
				};
				list.Add(tile);
			}
			return list;

		}

	}

}