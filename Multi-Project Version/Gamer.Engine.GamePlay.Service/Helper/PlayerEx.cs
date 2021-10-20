using System.Collections.Generic;
using System.Linq;
using Gamer.Engine.GamePlay.Interface;

namespace Gamer.Engine.GamePlay.Service.Helper
{
	internal static class PlayerEx
	{

		public static Player Convert(this Access.Player.Interface.Player source)
		{

			var target = new Player
			{
				Id = source.Id,
				GamePiece = source.GamePiece,
				IsMachine = source.IsMachine,
				Name = source.Name
			};
			return target;

		}

		public static Access.Player.Interface.Player Convert(this Player source)
		{

			var target = new Access.Player.Interface.Player
			{
				Id = source.Id,
				GamePiece = source.GamePiece,
				IsMachine = source.IsMachine,
				Name = source.Name
			};
			return target;

		}

		public static Player[] Convert(this IEnumerable<Access.Player.Interface.Player> source)
		{
			return source.Select(i => i.Convert()).ToArray();
		}

		public static Access.Player.Interface.Player[] Convert(this IEnumerable<Player> source)
		{
			return source.Select(i => i.Convert()).ToArray();
		}

	}
}