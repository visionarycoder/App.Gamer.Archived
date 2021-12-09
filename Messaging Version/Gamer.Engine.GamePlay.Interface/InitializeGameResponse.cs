using Gamer.Utility.ServiceMessaging;

namespace Gamer.Engine.GamePlay.Interface
{
	public class InitializeGameResponse : ServiceMessageResponse
	{
		public GameSession GameSession { get; set; }
	}
}