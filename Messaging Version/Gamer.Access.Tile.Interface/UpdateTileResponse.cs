using Gamer.Framework.ServiceMessaging;

namespace Gamer.Access.Tile.Interface
{
	public class UpdateTileResponse : ServiceMessageResponse
	{
		public Tile Tile { get; set; }
	}
}