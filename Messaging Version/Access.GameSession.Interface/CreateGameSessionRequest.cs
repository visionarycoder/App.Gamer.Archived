using System;
using Util.ServiceMessaging;

namespace Access.GameSession.Interface
{
    public class CreateGameSessionRequest : ServiceMessageRequest
    {
        public Guid[] PlayerIds { get; set; } = Array.Empty<Guid>();
    }
}