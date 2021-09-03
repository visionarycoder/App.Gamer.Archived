using Util.ServiceMessaging;

namespace Access.GameSession.Interface
{
    public class ApplyGameSessionChangeResponse : ServiceMessageResponse
    {
        public GameSession GameSession { get; set; }

    }
}