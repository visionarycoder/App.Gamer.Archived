using Gamer.Framework.ServiceMessaging;

namespace Gamer.Access.Tile.Interface
{
	public class CreateTilesResponse : ServiceMessageResponse
	{
		public Tile[] Tiles { get; set; }
	}
}