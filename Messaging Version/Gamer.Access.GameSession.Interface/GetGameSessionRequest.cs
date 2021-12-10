using System;
using Gamer.Utility.ServiceMessaging;

namespace Gamer.Access.GameSession.Interface
{
	public class GetGameSessionRequest : ServiceMessageRequest
	{
		public Guid GameSessionId { get; set; }
	}
}