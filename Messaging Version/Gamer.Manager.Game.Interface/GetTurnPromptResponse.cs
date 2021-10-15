using Gamer.Framework.ServiceMessaging;

namespace Gamer.Manager.Game.Interface
{
	public class GetTurnPromptResponse : ServiceMessageResponse
	{
		public string Prompt { get; set; }
	}
}