using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Gamer.Manager.Game.Interface;

namespace Gamer.Manager.Game.Service.Helpers
{
	public static class PlayerEx
	{

		public static Player Convert(this Access.Player.Interface.Player source)
		{
			Contract.Assert(source != null, "Input is null.");
			var target = new Player
			{
				GamePiece = source.GamePiece,
				Id = source.Id,
				IsMachine = source.IsMachine,
				Name = source.Name,
			};
			return target;

		}

		public static Player Convert(this Engine.GamePlay.Interface.Player source)
		{

			Contract.Assert(source != null, "Input is null.");
			var target = new Player
			{
				GamePiece = source.GamePiece,
				Id = source.Id,
				IsMachine = source.IsMachine,
				Name = source.Name,
			};
			return target;

		}

		public static Access.Player.Interface.Player Convert(this Player source)
		{

			var target = new Access.Player.Interface.Player
			{
				GamePiece = source.GamePiece,
				Id = source.Id,
				IsMachine = source.IsMachine,
				Name = source.Name,
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