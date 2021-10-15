using Gamer.Framework.ServiceMessaging;

namespace Gamer.Access.GameSession.Interface
{
	public class CreateGameSessionResponse : ServiceMessageResponse
	{
		public GameSession GameSession { get; set; }
	}
}