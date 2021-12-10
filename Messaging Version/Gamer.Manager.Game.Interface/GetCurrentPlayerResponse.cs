using Gamer.Utility.ServiceMessaging;

namespace Gamer.Manager.Game.Interface
{
	public class GetCurrentPlayerResponse : ServiceMessageResponse
	{
		public Player Player { get; set; }
	}
}