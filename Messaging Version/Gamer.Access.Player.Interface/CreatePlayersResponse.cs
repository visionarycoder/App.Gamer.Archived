using Gamer.Framework.ServiceMessaging;

namespace Gamer.Access.Player.Interface
{
	public class CreatePlayersResponse : ServiceMessageResponse
	{
		public Player[] Players { get; set; }
	}
}