using Gamer.Framework.ServiceMessaging;

namespace Gamer.Engine.GamePlay.Interface
{
	public class FindWinnerResponse : ServiceMessageResponse
	{
		public Player Player { get; set; }
	}
}