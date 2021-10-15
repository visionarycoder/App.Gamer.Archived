using Gamer.Framework.ServiceMessaging;

namespace Gamer.Access.Player.Interface
{
	public class GetPlayerResponse : ServiceMessageResponse
	{
		public Player Player { get; set; }
	}
}