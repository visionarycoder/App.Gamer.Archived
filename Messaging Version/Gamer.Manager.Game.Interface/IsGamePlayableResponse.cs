using Gamer.Framework.ServiceMessaging;

namespace Gamer.Manager.Game.Interface
{
	public class IsGamePlayableResponse : ServiceMessageResponse
	{
		public bool IsPlayable { get; set; }
	}
}