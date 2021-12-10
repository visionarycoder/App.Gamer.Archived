using Gamer.Utility.ServiceMessaging;

namespace Gamer.Manager.Game.Interface
{
	public class FindWinnerResponse : ServiceMessageResponse
	{
		public Player Player { get; set; }
	}
}