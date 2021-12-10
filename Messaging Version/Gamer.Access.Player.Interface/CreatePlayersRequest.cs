using Gamer.Utility.ServiceMessaging;

namespace Gamer.Access.Player.Interface
{
	public class CreatePlayersRequest : ServiceMessageRequest
	{
		public Player[] Players { get; set; }
	}
}