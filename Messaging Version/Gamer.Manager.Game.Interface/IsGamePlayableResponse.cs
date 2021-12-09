using Gamer.Utility.ServiceMessaging;

namespace Gamer.Manager.Game.Interface
{
	public class IsGamePlayableResponse : ServiceMessageResponse
	{
		public bool IsPlayable { get; set; }
	}
}