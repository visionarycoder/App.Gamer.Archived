using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Tile = Gamer.Engine.GamePlay.Interface.Tile;

namespace Gamer.Engine.GamePlay.Service.Helper
{

	internal static class TileEx
	{
		
		public static Tile Convert(this Access.Tile.Interface.Tile source)
		{

			Contract.Assert(source != null, "Input is null.");
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

			Contract.Assert(source != null, "Input is null.");
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
			Contract.Assert(source != null, "Input is null.");
			return source.Select(i => i.Convert()).ToArray();
		}

		public static Access.Tile.Interface.Tile[] Convert(this IEnumerable<Tile> source)
		{
			Contract.Assert(source != null, "Input is null.");
			return source.Select(i => i.Convert()).ToArray();
		}

	}

}