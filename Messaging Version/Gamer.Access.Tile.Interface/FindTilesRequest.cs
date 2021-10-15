using System;
using Gamer.Framework.ServiceMessaging;

namespace Gamer.Access.Tile.Interface
{
	public class FindTilesRequest : ServiceMessageRequest
	{
		public Func<Tile, bool> Filter { get; set; }
	}
}