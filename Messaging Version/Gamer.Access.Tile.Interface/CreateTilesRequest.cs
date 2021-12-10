using Gamer.Utility.ServiceMessaging;

namespace Gamer.Access.Tile.Interface
{
	public class CreateTilesRequest : ServiceMessageRequest
	{
		public Tile[] Tiles { get; set; }
	}
}