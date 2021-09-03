using Util.ServiceMessaging;

namespace Access.GameSession.Interface
{
    public class RetrieveGameSessionResponse : ServiceMessageResponse
    {
        public GameSession GameSession { get; set; }
    }
}