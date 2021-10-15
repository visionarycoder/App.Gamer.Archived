using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Gamer.Engine.GamePlay.Interface;

namespace Gamer.Engine.GamePlay.Service.Helper
{
	internal static class PlayerEx
	{

		public static Player Convert(this Access.Player.Interface.Player source)
		{

			Contract.Assert(source != null, "Input is null.");
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

			Contract.Assert(source != null, "Input is null.");
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
			Contract.Assert(source != null, "Input is null.");
			return source.Select(i => i.Convert()).ToArray();
		}

		public static Access.Player.Interface.Player[] Convert(this IEnumerable<Player> source)
		{
			Contract.Assert(source != null, "Input is null.");
			return source.Select(i => i.Convert()).ToArray();
		}

	}
}