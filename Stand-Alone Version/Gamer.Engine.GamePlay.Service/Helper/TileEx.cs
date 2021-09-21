using System.Collections.Generic;
using System.Linq;

namespace Gamer.Engine.GamePlay.Service.Helper
{

	internal static class TileEx
	{
		
		public static Tile Convert(this Access.Tile.Interface.Tile source)
		{

			var target = new Tile
			{
				Address = source.Address,
				GameSessionId = source.GameSessionId,
				Id = source.Id,
				PlayerId = source.PlayerId,
			};
			return target;

		}

		public static Access.Tile.Interface.Tile Convert(this Tile source)
		{
			
			var target = new Access.Tile.Interface.Tile
			{
				Address = source.Address,
				GameSessionId = source.GameSessionId,
				Id = source.Id,
				PlayerId = source.PlayerId,
			};
			return target;

		}

		public static Tile[] Convert(this IEnumerable<Access.Tile.Interface.Tile> source)
		{
			return source.Select(i => i.Convert()).ToArray();
		}

		public static Access.Tile.Interface.Tile[] Convert(this IEnumerable<Tile> source)
		{
			return source.Select(i => i.Convert()).ToArray();
		}

	}

}