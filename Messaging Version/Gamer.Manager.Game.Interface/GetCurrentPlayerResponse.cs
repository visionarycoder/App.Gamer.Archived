using Gamer.Framework.ServiceMessaging;

namespace Gamer.Manager.Game.Interface
{
	public class GetCurrentPlayerResponse : ServiceMessageResponse
	{
		public Player Player { get; set; }
	}
}