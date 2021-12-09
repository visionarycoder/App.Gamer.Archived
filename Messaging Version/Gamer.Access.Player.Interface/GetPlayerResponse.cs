using Gamer.Utility.ServiceMessaging;

namespace Gamer.Access.Player.Interface
{
	public class GetPlayerResponse : ServiceMessageResponse
	{
		public Player Player { get; set; }
	}
}