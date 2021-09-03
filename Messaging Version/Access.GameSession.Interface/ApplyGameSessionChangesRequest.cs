using Util.ServiceMessaging;

namespace Access.GameSession.Interface
{
    public class ApplyGameSessionChangesRequest : ServiceMessageRequest
    {
        
        public GameSession GameSession { get; set; }

    }
}