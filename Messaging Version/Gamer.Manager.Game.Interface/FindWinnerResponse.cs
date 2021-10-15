using Gamer.Framework.ServiceMessaging;

namespace Gamer.Manager.Game.Interface
{
	public class FindWinnerResponse : ServiceMessageResponse
	{
		public Player Player { get; set; }
	}
}