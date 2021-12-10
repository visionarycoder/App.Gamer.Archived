using Gamer.Utility.ServiceMessaging;

namespace Gamer.Access.Tile.Interface
{
	public class UpdateTileResponse : ServiceMessageResponse
	{
		public Tile Tile { get; set; }
	}
}