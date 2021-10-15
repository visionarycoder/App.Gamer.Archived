using System;
using Gamer.Engine.GamePlay.Interface;

namespace Gamer.Engine.GamePlay.Service.Factory
{

	public static class PlayerFactory
	{
	
		public static Player Create(string name, string gamePiece, bool isMachine = false)
		{
		
			var target = new Player
			{
				Id = Guid.NewGuid(),
				Name = name,
				GamePiece = gamePiece,
				IsMachine = isMachine
			};
			return target;

		}

	}

}
