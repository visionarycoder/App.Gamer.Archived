using Gamer.Framework.ServiceMessaging;

namespace Gamer.Access.Player.Interface
{
	public class CreatePlayersRequest : ServiceMessageRequest
	{
		public Player[] Players { get; set; }
	}
}