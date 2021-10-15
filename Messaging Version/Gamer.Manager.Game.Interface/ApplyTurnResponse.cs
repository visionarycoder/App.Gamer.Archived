using Gamer.Framework.ServiceMessaging;

namespace Gamer.Manager.Game.Interface
{
	public class ApplyTurnResponse : ServiceMessageResponse
	{
		public string Prompt { get; set; }
	}
}