using Gamer.Framework.ServiceMessaging;

namespace Gamer.Access.GameSession.Interface
{
	public class GetGameSessionResponse : ServiceMessageResponse
	{
		public GameSession GameSession { get; set; }
	}
}