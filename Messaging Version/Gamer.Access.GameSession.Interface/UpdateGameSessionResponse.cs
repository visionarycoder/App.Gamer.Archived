using Gamer.Utility.ServiceMessaging;

namespace Gamer.Access.GameSession.Interface
{
	public class UpdateGameSessionResponse : ServiceMessageResponse
	{
		public GameSession GameSession { get; set; }
	}
}