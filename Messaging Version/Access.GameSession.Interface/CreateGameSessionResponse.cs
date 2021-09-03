using Util.ServiceMessaging;

namespace Access.GameSession.Interface
{
    public class CreateGameSessionResponse : ServiceMessageResponse
    {
        public GameSession GameSession { get; set; }
    }
}