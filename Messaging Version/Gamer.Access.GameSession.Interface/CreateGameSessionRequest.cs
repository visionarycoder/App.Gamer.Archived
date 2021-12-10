using Gamer.Utility.ServiceMessaging;

namespace Gamer.Access.GameSession.Interface
{
    public class CreateGameSessionRequest : ServiceMessageRequest
    {
        public GameSession GameSession { get; set; }
    }
}