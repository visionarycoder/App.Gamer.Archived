using Gamer.Framework.ServiceMessaging;

namespace Gamer.Access.GameSession.Interface
{
	public class UpdateGameSessionRequest : ServiceMessageRequest
	{
		public GameSession GameSession { get; set; }
	}
}