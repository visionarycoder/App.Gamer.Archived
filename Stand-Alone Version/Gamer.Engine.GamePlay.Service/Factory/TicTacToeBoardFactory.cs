using System;
using System.Collections.Generic;
using Tile = Gamer.Engine.GamePlay.Interface.Tile;

namespace Gamer.Engine.GamePlay.Service.Factory
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