using Gamer.Utility.ServiceMessaging;

namespace Gamer.Manager.Game.Interface
{
	public class GetTurnPromptResponse : ServiceMessageResponse
	{
		public string Prompt { get; set; }
	}
}