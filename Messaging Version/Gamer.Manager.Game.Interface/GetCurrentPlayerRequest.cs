using System;
using Gamer.Utility.ServiceMessaging;

namespace Gamer.Manager.Game.Interface
{
	public class GetCurrentPlayerRequest : ServiceMessageRequest
	{
		public Guid GameSessionId { get; set; }
	}
}