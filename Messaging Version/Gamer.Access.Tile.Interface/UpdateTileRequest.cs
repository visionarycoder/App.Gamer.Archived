using Gamer.Framework.ServiceMessaging;

namespace Gamer.Access.Tile.Interface
{
	public class UpdateTileRequest : ServiceMessageRequest
	{
		public Tile Tile { get; set; }
	}
}