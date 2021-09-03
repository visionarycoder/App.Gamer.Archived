using System;
using Util.ServiceMessaging;

namespace Access.GameSession.Interface
{
    public class RetrieveGameSessionRequest : ServiceMessageRequest
    {
        public Guid GameSessionId { get; set; }
    }
}